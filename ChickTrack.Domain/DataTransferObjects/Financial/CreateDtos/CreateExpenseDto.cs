namespace Domain.DataTransferObjects.Financial
{
    public class CreateExpenseDto
    {
        public string Code { get; set; }
        public DateOnly Date { get; set; }
        public string InvestorId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
