using AutoMapper;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Service.Interfaces;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Services.Implementation;

namespace ChickTrack.Service.Implementations
{
    public class ExpensesService : MSSQLBaseService<Expense, long>, IExpensesService
    {
        public ExpensesService(
            IMSSQLRepository<Expense, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
