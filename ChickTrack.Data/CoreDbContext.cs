using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Domain.Entities.Poultry;
using Microsoft.EntityFrameworkCore;

namespace ChickTrack.Data
{
    public class CoreDbContext : ApplicationDbContext
    {
        public CoreDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Investment> Investments { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Birds> Poultries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Investment>()
                .ToTable("Investments")
                .HasKey(x => x.Code);
            builder.Entity<Expense>()
                .ToTable("Expenses")
                .HasKey(x => x.Code);
            builder.Entity<Birds>()
                .ToTable("Poultries")
                .HasKey(x => x.Code);
            base.OnModelCreating(builder);
        }

    }
}
