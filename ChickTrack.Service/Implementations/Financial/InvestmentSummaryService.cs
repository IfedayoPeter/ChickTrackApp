using AutoMapper;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Service.Interfaces.Financial;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Services.Implementation;

namespace ChickTrack.Service.Implementations.Financial
{
    public class InvestmentSummaryService : MSSQLBaseService<InvestmentSummary, long>, IInvestmentSummaryService
    {
        public InvestmentSummaryService(
            IMSSQLRepository<InvestmentSummary, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
