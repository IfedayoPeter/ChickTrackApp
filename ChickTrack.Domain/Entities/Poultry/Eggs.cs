using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class Eggs : BaseEntity<long>
    {
        public int Count { get; set; }
        public DateOnly Date { get; set; }
    }
}
