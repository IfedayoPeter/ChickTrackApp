using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Financials
{
    public class TotalSales : BaseEntity<long>
    {
        public FeedBrandEnum FeedBrand { get; set; }
        public int BagsSold { get; set; }
        public decimal Amount { get; set; }
        public decimal Profit { get; set; }
    }
}
