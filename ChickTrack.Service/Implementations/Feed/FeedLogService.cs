namespace ChickTrack.Service.Implementations.Feed
{
    public class FeedLogService : MSSQLBaseService<FeedLog, long>, IFeedLogService
    {
        public FeedLogService(
            IMSSQLRepository<FeedLog, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
