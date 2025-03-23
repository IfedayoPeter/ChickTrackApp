using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Financial
{
    public class SaleRecordDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public SalesTypeEnum SalesType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly Date { get; set; }
        public string BuyerName { get; set; }
        public string Description { get; set; }
        public FeedBrandEnum? FeedBrand { get; set; }
        public long? FeedSalesUnitId { get; set; }
    }
}
