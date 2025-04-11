namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class CreateEggInventoryDto
    {
        public string Code { get; set; }
        public string InvestorId { get; set; }
        public int Sold { get; set; }
        public int Hatched { get; set; }
        public int PersonalCollection { get; set; }
        public int TotalEggs { get; set; }
        public int AvailableEggs { get; set; }
    }
}
