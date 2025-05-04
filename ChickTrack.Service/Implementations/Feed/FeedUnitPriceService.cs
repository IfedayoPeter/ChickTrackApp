using AutoMapper;
using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Service.Interfaces.Feed;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Services.Implementation;

namespace ChickTrack.Service.Implementations.Feed
{
    public class FeedUnitPriceService : MSSQLBaseService<FeedUnitPrice, long>, IFeedUnitPriceService
    {
        private readonly IMSSQLRepository<FeedUnitPrice, long> _feedUnitPrice;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public FeedUnitPriceService(
            IMSSQLRepository<FeedUnitPrice, long> feedUnitPrice,
            IApplicationDbContext context,
            IMapper mapper) : base(feedUnitPrice, context, mapper)
        {
            _feedUnitPrice = feedUnitPrice;
            _mapper = mapper;
            _context = context;
        }


        public async Task<Result<List<FeedUnitPriceDto>>> GetActivePricesAsync()
        {
            var result = new Result<List<FeedUnitPriceDto>>(false);
            try
            {
                //var prices = _feedUnitPrice.GetAllAsync().Result.Where(x => x.IsActive).ToList();
                var prices = await _feedUnitPrice.GetAllAsync(x => x.IsActive);
                if (prices == null)
                {
                    result.SetError("No active prices found", "No active prices found");
                }
                else
                {
                    result.SetSuccess(_mapper.Map<List<FeedUnitPriceDto>>(prices), "Active prices retrieved successfully!");
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
