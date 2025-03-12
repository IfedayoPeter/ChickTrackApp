using ChickTrack.Domain.Entities.Personnels;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class Birds : BaseEntity<long>
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int MaleBirds { get; set; }
        public int FemaleBirds { get; set; }
        public int BirdsSold { get; set; }
        public int BirdsLost { get; set; }

        public int TotalAvailableBirds => MaleBirds + FemaleBirds - BirdsSold - BirdsLost;
        public int TotalBirds => MaleBirds + FemaleBirds + BirdsSold + BirdsLost;
    }
}
