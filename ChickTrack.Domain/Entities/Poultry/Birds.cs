using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class Birds : BaseEntity<long>
    {
        public string InvestorId { get; set; }
        public int MaleBirds { get; set; }
        public int FemaleBirds { get; set; }
        public int Chicks { get; set; }
        public int BirdsSold { get; set; }
        public int BirdsLost { get; set; }
        public int PersonalConsumption { get; set; }

        public int TotalAvailableBirds => MaleBirds + FemaleBirds + Chicks - BirdsSold - BirdsLost - PersonalConsumption;
        public int TotalBirds => MaleBirds + FemaleBirds + Chicks + BirdsSold + BirdsLost + PersonalConsumption;

        public virtual BaseUser Investor { get; set; }
    }
}
