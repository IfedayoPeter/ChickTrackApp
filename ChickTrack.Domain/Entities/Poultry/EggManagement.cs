using ChickTrack.Base.Domain.Entities;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class EggManagement : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public BaseUser User { get; set; }
        public int ActionType { get; set; } // Add, Remove, Sell
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}
