using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Financial
{
    public class TotalSalesDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public FeedBrandEnum FeedBrand { get; set; }
        public decimal BagsSold { get; set; }
        public decimal Amount { get; set; }
        public decimal Profit { get; set; }
    }
}
