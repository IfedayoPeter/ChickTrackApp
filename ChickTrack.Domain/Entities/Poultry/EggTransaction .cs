using ChickTrack.Base.Domain.Entities;
using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Poultry
{
    public class EggTransaction : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string? InvestorId { get; set; }
        public ActionTypeEnum ActionType { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

    }
}
