namespace Domain.Entities.Feed
{
    public class FeedUnitPrice : BaseEntity<long>
    {
        public FeedBrandEnum FeedBrandEnum { get; set; }
        public FeedUnitEnum FeedUnitEnum { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
