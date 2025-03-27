using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Feed
{
    public class FeedInventory : BaseEntity<long>
    {
        public FeedBrandEnum FeedBrand { get; set; }
        public decimal BagsBought { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
