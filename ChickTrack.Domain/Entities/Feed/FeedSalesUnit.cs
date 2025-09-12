namespace Domain.Entities.Feed
{
    public class FeedSalesUnit : BaseEntity<long>
    {
        public string unitName { get; set; }

        [Column(TypeName = "decimal(18,9)")]
        public decimal unitQuantity { get; set; }
    }
}
