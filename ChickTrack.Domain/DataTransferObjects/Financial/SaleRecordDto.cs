using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Financial
{
    public class SaleRecordDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public SalesTypeEnum SalesType { get; set; }
        public string SalesTypeName() => SalesType.ToString();
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly Date { get; set; }
        public string BuyerName { get; set; }
        public string Description { get; set; }
        public FeedBrandEnum? FeedBrand { get; set; }
        public string FeedBrandName() => FeedBrand.ToString();
        public long? FeedSalesUnitId { get; set; }
        public decimal FeedSalesUnit { get; set; }
        public string FeedSalesUnitName { get; set; }
    }
}
