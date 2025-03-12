using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Financials
{
    public class SaleRecord : BaseEntity<long>
    {
        public required SalesTypeEnum SalesType { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
        public required DateOnly Date { get; set; }
        public string BuyerName { get; set; }
        public string Description { get; set; }

        public FeedBrandEnum? FeedBrand { get; set; }
        public long? FeedSalesUnitId { get; set; }

        public virtual FeedSalesUnit FeedSalesUnit { get; set; }
    }
}
