namespace ChickTrack.Domain.DataTransferObjects.Financial
{
    public class InvestmentDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ReceiptImageUrl { get; set; }
    }
}
