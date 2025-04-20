using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos
{
    public class EggTransactionDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateOnly Date { get; set; }
        public string? InvestorId { get; set; }
        public ActionTypeEnum ActionType { get; set; }
        public string ActionTypeName { get { return ActionType.ToString(); } }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}
