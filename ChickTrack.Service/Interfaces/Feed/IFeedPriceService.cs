namespace ChickTrack.Service.Interfaces.Feed
{
    public interface IFeedPriceService : IMSSQLBaseService<FeedPrice, long>
    {
        Task<Result<List<FeedPriceDto>>> GetActivePricesAsync();
    }
}
