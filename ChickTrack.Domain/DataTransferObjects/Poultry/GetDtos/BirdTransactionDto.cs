using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos
{
    public class BirdTransactionDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public string FullName { get; set; }
        public GenderEnum Gender { get; set; }
        public string GenderName { get { return Gender.ToString(); } }
        public ActionTypeEnum ActionType { get; set; }
        public string ActionTypeName { get { return ActionType.ToString(); } }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
