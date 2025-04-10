using ChickTrack.Base.Domain.Entities;
using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class BirdManagement : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public long BirdTransactionId { get; set; }
        public ActionTypeEnum ActionType { get; set; } //sell, hatch, loss, personal consumption
        public GenderEnum Gender { get; set; }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public virtual BaseUser Investor { get; set; }
    }
}
