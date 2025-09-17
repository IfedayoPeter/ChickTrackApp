namespace Domain.Entities.Financials
{
    public class Expense : BaseEntity<long>
    {
        public DateOnly Date { get; set; }
        public string InvestorId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public virtual BaseUser Investor { get; set; }
    }
}
