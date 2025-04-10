using ChickTrack.Base.Domain.Entities;
using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class EggManagement : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public long EggTransactionId { get; set; }
        public long EggInventoryId { get; set; }
        public ActionTypeEnum ActionType { get; set; } 
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public virtual BaseUser Investor { get; set; }
    }
}
