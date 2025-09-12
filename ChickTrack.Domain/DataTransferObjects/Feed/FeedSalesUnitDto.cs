namespace Domain.DataTransferObjects.Feed
{
    public class FeedSalesUnitDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string UnitName { get; set; }
        public decimal UnitQuantity { get; set; }
    }
}
