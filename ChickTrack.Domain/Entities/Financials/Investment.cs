using ChickTrack.Domain.Entities.Personnels;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Financials
{
    public class Investment : BaseEntity<long>
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ReceiptImageUrl { get; set; }
    }
}
