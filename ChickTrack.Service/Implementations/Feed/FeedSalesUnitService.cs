using AutoMapper;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces.Poultry;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Services.Implementation;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Service.Interfaces.Feed;

namespace ChickTrack.Service.Implementations.Feed
{
    public class FeedSalesUnitService : MSSQLBaseService<FeedSalesUnit, long>, IFeedSalesUnitService
    {
        public FeedSalesUnitService(
            IMSSQLRepository<FeedSalesUnit, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
