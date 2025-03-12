using Lagetronix.Rapha.Base.Common.Domain.Utilities;

namespace ChickTrack.Domain.DataTransferObjects
{
    public class InvestmentDTO
    {
        public long Id { get; set; } 
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ReceiptImageUrl { get; set; }
    }

}
