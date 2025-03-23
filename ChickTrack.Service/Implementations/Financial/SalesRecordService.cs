using AutoMapper;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Service.Interfaces.Financial;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Services.Implementation;

namespace ChickTrack.Service.Implementations.Financial
{
    public class SalesRecordService : MSSQLBaseService<SaleRecord, long>, ISaleRecordService
    {
        public SalesRecordService(
            IMSSQLRepository<SaleRecord, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
