using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Financials
{
    public class Investment : BaseEntity<long>
    {
        public string InvestorId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public string ReceiptImageUrl { get; set; }

        public virtual BaseUser Investor { get; set; }
    }
}
