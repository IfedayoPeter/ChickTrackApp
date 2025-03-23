using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class EggTransaction : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public BaseUser User { get; set; }
        public int Hatched { get; set; }
        public int Sold { get; set; }
        public int PersonalCollection { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
    }
}
