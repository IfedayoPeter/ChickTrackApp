using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Feed
{
    public class FeedSalesUnit : BaseEntity<long>
    {
        public string unitName { get; set; }
        public decimal unitQuantity { get; set; }
    }
}
