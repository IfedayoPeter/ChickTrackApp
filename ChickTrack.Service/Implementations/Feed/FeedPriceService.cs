namespace ChickTrack.Service.Implementations.Feed
{
    public class FeedPriceService : MSSQLBaseService<FeedPrice, long>, IFeedPriceService
    {
        private readonly IMSSQLRepository<FeedPrice, long> _feedPrice;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public FeedPriceService(
            IMSSQLRepository<FeedPrice, long> feedPrice,
            IApplicationDbContext context,
            IMapper mapper) : base(feedPrice, context, mapper)
        {
            _feedPrice = feedPrice;
            _mapper = mapper;
            _context = context;
        }


        public async Task<Result<List<FeedPriceDto>>> GetActivePricesAsync()
        {
            var result = new Result<List<FeedPriceDto>>(false);
            try
            {
                //var prices = _feedPrice.GetAllAsync().Result.Where(x => x.IsActive).ToList();
                var prices = await _feedPrice.GetAllAsync(x => x.IsActive);
                if (prices == null)
                {
                    result.SetError("No active prices found", "No active prices found");
                }
                else
                {
                    result.SetSuccess(_mapper.Map<List<FeedPriceDto>>(prices), "Active prices retrieved successfully!");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while retrieving active prices");
            }
            //return Task.FromResult(result);
            return result;
        }
    }

}
