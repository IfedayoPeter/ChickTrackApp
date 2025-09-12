

namespace Domain.DataTransferObjects.Poultry
{
    public class CreateBirdTransactionDto
    {
        public string Code { get; set; }
        public DateOnly Date { get; set; }
        public string InvestorId { get; set; }
        public ActionTypeEnum ActionType { get; set; }
        public int Quantity { get; set; }
        public GenderEnum Gender { get; set; } 
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
