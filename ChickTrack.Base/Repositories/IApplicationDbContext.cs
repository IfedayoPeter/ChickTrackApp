namespace Lagetronix.Rapha.Base.Common.Repositories
{
    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}