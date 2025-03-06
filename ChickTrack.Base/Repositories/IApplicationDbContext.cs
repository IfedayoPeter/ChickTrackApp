namespace Lagetronix.Rapha.Base.Common.Repositories
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}