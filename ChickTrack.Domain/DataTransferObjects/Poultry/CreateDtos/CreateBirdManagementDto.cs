using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class CreateBirdManagementDto
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public ActionTypeEnum ActionType{ get; set; } 
        public GenderEnum Gender { get; set; }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}
