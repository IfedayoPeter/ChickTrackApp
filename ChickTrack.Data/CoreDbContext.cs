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
                .HasKey(x => x.Code);
            builder.Entity<FeedLog>()
                .ToTable("FeedLogs")
                .HasKey(x => x.Code);
            builder.Entity<FeedSalesUnit>()
                .ToTable("FeedSalesUnits")
                .HasKey(x => x.Code);

            // Financials
            builder.Entity<Investment>()
                .ToTable("Investments")
                .HasKey(x => x.Code);
            builder.Entity<Expense>()
                .ToTable("Expenses")
                .HasKey(x => x.Code);
            builder.Entity<InvestmentSummary>()
                .ToTable("InvestmentSummaries")
                .HasKey(x => x.Code);
            builder.Entity<SaleRecord>()
                .ToTable("SaleRecords")
                .HasKey(x => x.Code);
            builder.Entity<TotalSales>()
                .ToTable("TotalSales")
                .HasKey(x => x.Code);

            // Poultry
            builder.Entity<BirdManagement>()
                .ToTable("BirdManagements")
                .HasKey(x => x.Code);
            builder.Entity<Birds>()
                .ToTable("Birds")
                .HasKey(x => x.Code);
            builder.Entity<BirdTransaction>()
                .ToTable("BirdTransactions")
                .HasKey(x => x.Code);
            builder.Entity<EggInventory>()
                .ToTable("EggInventories")
                .HasKey(x => x.Code);
            builder.Entity<EggManagement>()
                .ToTable("EggManagements")
                .HasKey(x => x.Code);
            builder.Entity<EggTransaction>()
                .ToTable("EggTransactions")
                .HasKey(x => x.Code);

            base.OnModelCreating(builder);
        }

    }
}
