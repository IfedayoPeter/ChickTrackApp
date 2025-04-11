using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class CreateBirdTransactionDto
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public ActionTypeEnum ActionType { get; set; }
        public int Quantity { get; set; }
        public GenderEnum Gender { get; set; } 
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
