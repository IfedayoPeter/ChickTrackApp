using AutoMapper;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces.Poultry;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Services.Implementation;

namespace ChickTrack.Service.Implementations.Poultry
{
    public class BirdManagementService : MSSQLBaseService<BirdManagement, long>, IBirdManagementService
    {
        public BirdManagementService(
            IMSSQLRepository<BirdManagement, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
