namespace ChickTrack.Service.Implementations.Feed
{
    public class FeedSalesUnitService : MSSQLBaseService<FeedSalesUnit, long>, IFeedSalesUnitService
    {
        public FeedSalesUnitService(
            IMSSQLRepository<FeedSalesUnit, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
