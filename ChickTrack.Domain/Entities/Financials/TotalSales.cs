namespace Domain.Entities.Financials
{
    public class TotalSales : BaseEntity<long>
    {
        public FeedBrandEnum FeedBrand { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal BagsSold { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal Profit { get; set; }
    }
}
