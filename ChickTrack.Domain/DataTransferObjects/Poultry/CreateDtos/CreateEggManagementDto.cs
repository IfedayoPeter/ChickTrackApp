namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class CreateEggManagementDto
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int ActionType { get; set; } // Add, Remove, Sell
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}
