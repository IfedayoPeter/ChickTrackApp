namespace ChickTrack.Service.Implementations.Poultry
{
    public class EggInventoryService : MSSQLBaseService<EggInventory, long>, IEggInventoryService
    {
        public EggInventoryService(
            IMSSQLRepository<EggInventory, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
