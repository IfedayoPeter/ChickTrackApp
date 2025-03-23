namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class EggTransactionDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public int Hatched { get; set; }
        public int Sold { get; set; }
        public int PersonalCollection { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
    }
}
