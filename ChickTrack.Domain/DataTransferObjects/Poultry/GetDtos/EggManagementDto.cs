namespace ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos
{
    public class EggManagementDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string InvestorId { get; set; }
        public string FullName { get; set; }
        public int ActionType { get; set; } // Add, Remove, Sell
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}
