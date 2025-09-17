

namespace Domain.DataTransferObjects.Financial.GetDtos
{
    public class ExpenseDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateOnly Date { get; set; }
        public string InvestorId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }


    }
}
