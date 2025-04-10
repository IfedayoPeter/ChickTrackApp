using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class EggTransaction : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public int Hatched { get; set; }
        public int Sold { get; set; }
        public int PersonalConsumption { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }

        public virtual BaseUser Investor { get; set; }
    }
}
