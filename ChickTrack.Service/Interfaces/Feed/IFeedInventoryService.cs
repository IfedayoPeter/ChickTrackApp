using ChickTrack.Domain.Entities.Feed;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces.Feed
{
    public interface IFeedInventoryService : IMSSQLBaseService<FeedInventory, long>
    {
    }
}
