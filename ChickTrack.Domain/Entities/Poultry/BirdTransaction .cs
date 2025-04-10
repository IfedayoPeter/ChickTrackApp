using ChickTrack.Base.Domain.Entities;
using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class BirdTransaction : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public int PersonalConsumption { get; set; }
        public int HatchedBirds { get; set; }
        public int BirdsLost { get; set; }
        public int BirdsSold { get; set; }
        public GenderEnum Gender { get; set; } 
        public decimal? Amount { get; set; }

        public virtual BaseUser Investor { get; set; }
    }
}
