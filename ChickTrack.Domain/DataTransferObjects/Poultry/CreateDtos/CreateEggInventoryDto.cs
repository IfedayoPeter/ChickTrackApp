namespace Domain.DataTransferObjects.Poultry
{
    public class CreateEggInventoryDto
    {
        public string Code { get; set; }
        public int Sold { get; set; }
        public int Hatched { get; set; }
        public int PersonalConsumption { get; set; }
        public int stock { get; set; }
    }
}
