using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Financials
{
    public class Expense : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public virtual BaseUser Investor { get; set; }
    }
}
