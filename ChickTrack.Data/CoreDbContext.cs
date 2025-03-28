using ChickTrack.Domain.Entities.Feed;
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
        //Feed
        public DbSet<FeedInventory> FeedInventories { get; set; }
        public DbSet<FeedLog> FeedLogs { get; set; }
        public DbSet<FeedSalesUnit> FeedSalesUnits { get; set; }

        //Financials
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<InvestmentSummary> InvestmentSummaries { get; set; }
        public DbSet<SaleRecord> SaleRecords { get; set; }
        public DbSet<TotalSales> TotalSales { get; set; }

        //Poultry
        public DbSet<BirdManagement> BirdManagements { get; set; }
        public DbSet<Birds> Birds { get; set; }
        public DbSet<BirdTransaction> BirdTransactions { get; set; }
        public DbSet<EggInventory> EggInventories { get; set; }
        public DbSet<EggManagement> EggManagements { get; set; }
        public DbSet<EggTransaction> EggTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Feed
            builder.Entity<FeedInventory>()
                .ToTable("FeedInventories")
                .HasKey(x => x.Id);
            builder.Entity<FeedLog>()
                .ToTable("FeedLogs")
                .HasKey(x => x.Id);
            builder.Entity<FeedSalesUnit>()
                .ToTable("FeedSalesUnits")
                .HasKey(x => x.Id);

            // Financials
            builder.Entity<Investment>()
                .ToTable("Investments")
                .HasKey(x => x.Id);
            builder.Entity<Expense>()
                .ToTable("Expenses")
                .HasKey(x => x.Id);
            builder.Entity<InvestmentSummary>()
                .ToTable("InvestmentSummaries")
                .HasKey(x => x.Id);
            builder.Entity<SaleRecord>()
                .ToTable("SaleRecords")
                .HasKey(x => x.Id);
            builder.Entity<TotalSales>()
                .ToTable("TotalSales")
                .HasKey(x => x.Id);

            // Poultry
            builder.Entity<BirdManagement>()
                .ToTable("BirdManagements")
                .HasKey(x => x.Id);
            builder.Entity<Birds>()
                .ToTable("Birds")
                .HasKey(x => x.Id);
            builder.Entity<BirdTransaction>()
                .ToTable("BirdTransactions")
                .HasKey(x => x.Id);
            builder.Entity<EggInventory>()
                .ToTable("EggInventories")
                .HasKey(x => x.Id);
            builder.Entity<EggManagement>()
                .ToTable("EggManagements")
                .HasKey(x => x.Id);
            builder.Entity<EggTransaction>()
                .ToTable("EggTransactions")
                .HasKey(x => x.Id);

            base.OnModelCreating(builder);
        }

    }
}
