

namespace ChickTrack.Service.Implementations.Poultry
{
    public class BirdService : MSSQLBaseService<Birds, long>, IBirdService
    {
        public BirdService(
            IMSSQLRepository<Birds, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
