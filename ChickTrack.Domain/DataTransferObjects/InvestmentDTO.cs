using Lagetronix.Rapha.Base.Common.Domain.Utilities;

namespace ChickTrack.Domain.DataTransferObjects
{
    public class InvestmentDTO
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ReceiptImageUrl { get; set; }
    }
}
