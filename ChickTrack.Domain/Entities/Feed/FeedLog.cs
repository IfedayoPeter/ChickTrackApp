

namespace Domain.Entities.Feed
{
    public class FeedLog : BaseEntity<long>
    {
        public FeedBrandEnum FeedBrand { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal BagsBought { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal BagsSold { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal AvailableBags { get; set; }
    }
}
