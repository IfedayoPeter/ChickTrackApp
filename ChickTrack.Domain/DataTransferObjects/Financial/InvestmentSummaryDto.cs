namespace ChickTrack.Domain.DataTransferObjects.Financial
{
    public class InvestmentSummaryDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string InvestorId { get; set; }
        public string InvestorFullName { get; set; }
        public decimal TotalInvestment { get; set; }
        public decimal TotalExpenses { get; set; }
    }
}
