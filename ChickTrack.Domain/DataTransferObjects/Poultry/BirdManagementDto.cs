using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class BirdManagementDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public string InvestorName { get; }
        public ActionTypeEnum ActionType{ get; set; } 
        public string ActionTypeName { get { return ActionType.ToString(); } }
        public GenderEnum Gender { get; set; }
        public string GenderName { get { return Gender.ToString(); } }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public virtual Birds Bird { get; set; }
        public virtual BirdTransaction BirdTransaction { get; set; }
    }
}
