using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.Entities.Feed;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Services.Interface;

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
