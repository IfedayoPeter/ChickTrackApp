namespace Domain.DataTransferObjects.Financial.GetDtos
{
    public class CreateInvestmentDto
    {
        public string Code { get; set; }
        public string InvestorId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public string ReceiptImageUrl { get; set; }
    }
}
