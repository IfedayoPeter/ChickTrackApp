using ChickTrack.Base.Domain.Entities;
using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class BirdManagement : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string InvstorId { get; set; }
        public BaseUser User { get; set; }
        public ActionTypeEnum ActionType { get; set; } 
        public GenderEnum Gender { get; set; }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public virtual Birds Bird { get; set; }
        public virtual BirdTransaction BirdTransaction { get; set; }
    }
}
