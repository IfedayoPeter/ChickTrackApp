using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class EggInventory : BaseEntity<long>
    {
        public string InvestorId { get; set; }
        public int Sold { get; set; }
        public int Hatched { get; set; }
        public int PersonalConsumption { get; set; }

        public int TotalEggs => Sold + Hatched + PersonalConsumption;

        public virtual BaseUser Investor { get; set; }
    }
}
