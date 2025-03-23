using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class EggInventory : BaseEntity<long>
    {
        public string UserId { get; set; }
        public BaseUser User { get; set; }
        public int Sold { get; set; }
        public int Hatched { get; set; }
        public int PersonalCollection { get; set; }

        public int TotalEggs => Sold + Hatched + PersonalCollection;
        public int AvailableEggs => TotalEggs - Sold - Hatched - PersonalCollection;
    }
}
