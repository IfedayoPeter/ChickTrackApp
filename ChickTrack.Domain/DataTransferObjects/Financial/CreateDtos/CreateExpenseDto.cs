using ChickTrack.Base.Domain.Entities;

namespace ChickTrack.Domain.DataTransferObjects.Financial
{
    public class CreateExpenseDto
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
