namespace Domain.DataTransferObjects.Poultry.GetDtos
{
    public class EggInventoryDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public int Sold { get; set; }
        public int Hatched { get; set; }
        public int PersonalConsumption { get; set; }
        public int stock { get; set; }
        public int TotalEggs { get; set; }
    }
}
