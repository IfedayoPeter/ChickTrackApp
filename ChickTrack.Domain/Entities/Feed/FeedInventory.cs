


namespace Domain.Entities.Feed
{
    public class FeedInventory : BaseEntity<long>
    {
        public FeedBrandEnum FeedBrand { get; set; }
        public decimal BagsBought { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
    }
}
