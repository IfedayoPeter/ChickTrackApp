using ChickTrack.Base.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<BaseUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public override DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var currentTime = DateTime.Now;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.Entity == null) continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? "SYSTEM";
                    entry.Entity.CreatedOn = currentTime;
                    entry.Entity.LastModifiedBy = "SYSTEM";
                    entry.Entity.LastModifiedOn = currentTime;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = "SYSTEM";
                    entry.Entity.LastModifiedOn = currentTime;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}