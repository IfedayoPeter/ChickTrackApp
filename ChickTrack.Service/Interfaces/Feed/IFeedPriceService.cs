using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.Entities.Feed;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces.Feed
{
    public interface IFeedPriceService : IMSSQLBaseService<FeedPrice, long>
    {
        Task<Result<List<FeedPriceDto>>> GetActivePricesAsync();
    }
}
