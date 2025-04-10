using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Financials
{
    public class InvestmentSummary : BaseEntity<long>
    {
        public string InvestorId { get; set; }
        public decimal TotalInvestment { get; set; }
        public decimal TotalExpenses { get; set; }
        
        public decimal Balance => TotalInvestment - TotalExpenses;

        public virtual BaseUser Investor { get; set; }
    }
}
