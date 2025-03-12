using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Financials
{
    public class Expense : BaseEntity<long>
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
