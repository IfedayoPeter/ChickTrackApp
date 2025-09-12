namespace ChickTrack.Service.Implementations.Financial
{
    public class TotalSalesService : MSSQLBaseService<TotalSales, long>, ITotalSalesService
    {
        public TotalSalesService(
            IMSSQLRepository<TotalSales, long> baseRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(baseRepository, context, mapper)
        {
        }
    }
}
