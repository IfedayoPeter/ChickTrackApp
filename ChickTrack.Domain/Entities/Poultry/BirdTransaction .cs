using ChickTrack.Base.Domain.Entities;
using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class BirdTransaction : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public BaseUser User { get; set; }
        public int BirdsLost { get; set; }
        public int BirdsSold { get; set; }
        public GenderEnum Gender { get; set; } // Male, Female, Chicks
        public decimal Amount { get; set; }
    }
}
