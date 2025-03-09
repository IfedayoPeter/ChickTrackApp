using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities
{
    public class Poultry : BaseEntity<long>
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int TotalBirds { get; set; }
        public int MaleBirds { get; set; }
        public int FemaleBirds { get; set; }
    }
}
