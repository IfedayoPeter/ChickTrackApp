namespace Domain.Entities.Poultry
{
    public class EggInventory : BaseEntity<long>
    {
        public int Sold { get; set; }
        public int Hatched { get; set; }
        public int PersonalConsumption { get; set; }
        public int Stock { get; set; }

        public int TotalEggs => Sold + Hatched + PersonalConsumption + Stock;
    }
}
