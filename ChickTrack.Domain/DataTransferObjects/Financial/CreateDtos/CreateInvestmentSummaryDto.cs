namespace ChickTrack.Domain.DataTransferObjects.Financial.GetDtos
{
    public class CreateInvestmentSummaryDto
    {
        public string Code { get; set; }
        public string InvestorId { get; set; }
        public decimal TotalInvestment { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal Balance { get; set; }
    }
}
