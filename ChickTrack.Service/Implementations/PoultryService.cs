using AutoMapper;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Services.Implementation;

namespace ChickTrack.Service.Implementations
{
    public class PoultryService : MSSQLBaseService<Birds, long>, IPoultryService
    {
        public PoultryService(
            IMSSQLRepository<Birds, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
