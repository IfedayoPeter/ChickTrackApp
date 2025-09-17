namespace Domain.Entities.Feed
{
    public class FeedPrice : BaseEntity<long>
    {
        public FeedBrandEnum FeedBrandEnum { get; set; }
        public decimal PricePerBag { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
