namespace ChickTrack.Service.Implementations.Financial
{
    public class InvestmentService : MSSQLBaseService<Investment, long>, IInvestmentService
    {
        public InvestmentService(
            IMSSQLRepository<Investment, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
