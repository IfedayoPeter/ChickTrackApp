

namespace ChickTrack.Service.Interfaces.Feed
{
    public interface IFeedInventoryService : IMSSQLBaseService<FeedInventory, long>
    {
        Task<Result<FeedInventoryDto>> CreateAsync(FeedInventoryDto feedInventoryDto);
        Task<Result<FeedInventoryDto>> UpdateAsync(int id, FeedInventoryDto updatedDto);
        Task<Result<bool>> DeleteAsync(long id);
        Task<Result<List<long>>> ImportAsync(FeedInventoryDto[] feedInventoryDtos);
    }
}
