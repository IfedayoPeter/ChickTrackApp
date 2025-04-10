namespace ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos
{
    public class EggInventoryDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
        public int Sold { get; set; }
        public int Hatched { get; set; }
        public int PersonalCollection { get; set; }
        public int TotalEggs { get; set; }
        public int AvailableEggs { get; set; }
    }
}
