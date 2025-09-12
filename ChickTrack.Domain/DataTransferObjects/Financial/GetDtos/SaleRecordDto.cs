

namespace Domain.DataTransferObjects.Financial.GetDtos
{
    public class SaleRecordDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public SalesTypeEnum SalesType { get; set; }
        public string SalesTypeName { get { return SalesType.ToString(); } }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly Date { get; set; }
        public string BuyerName { get; set; }
        public string Description { get; set; }
        public FeedBrandEnum? FeedBrand { get; set; }
        public string FeedBrandName { get { return FeedBrand.ToString(); } }
        public long? FeedSalesUnitId { get; set; }

        public virtual FeedSalesUnit FeedSalesUnit { get; set; }
    }
}
