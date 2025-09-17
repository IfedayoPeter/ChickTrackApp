namespace ChickTrack.Service.Interfaces.Feed
{
    public interface IFeedUnitPriceService : IMSSQLBaseService<FeedUnitPrice, long>
    {
        Task<Result<List<FeedUnitPriceDto>>> GetActivePricesAsync();
    }
}
