namespace ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos
{
    public class EggInventoryDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string InvestorId { get; set; }
        public string FullName { get; set; }
        public int PersonalConsumption { get; set; }
        public int TotalEggs { get; set; }
        public int AvailableEggs { get; set; }
    }
}
