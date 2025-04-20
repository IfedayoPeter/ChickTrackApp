using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class CreateEggTransactionDto
    {
        public string Code { get; set; }
        public DateOnly Date { get; set; }
        public string? InvestorId { get; set; }
        public ActionTypeEnum ActionType { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}
