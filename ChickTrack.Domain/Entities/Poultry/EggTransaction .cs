namespace Domain.Entities.Poultry
{
    public class EggTransaction : BaseEntity<long>
    {
        public DateOnly Date { get; set; }
        public string? InvestorId { get; set; }
        public ActionTypeEnum ActionType { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

    }
}
