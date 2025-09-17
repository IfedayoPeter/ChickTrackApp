namespace ChickTrack.Service.Implementations.Financial
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
