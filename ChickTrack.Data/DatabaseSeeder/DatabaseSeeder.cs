

using Base.Domain.Entities;
using ChickTrack.Data;
using ChickTrack.Data.DatabaseSeeder.Data;

namespace BaseClassLibrary.Repositories.DatabaseSeeder
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(CoreDbContext context)
        {
            // Ensure the database is created
            //await context.Database.EnsureCreatedAsync();
            await SeedSuperAdminAsync(context, SuperAdminSeedData.GetSuperAdmins());
            await SeedFeedSalesUnitAsync(context, FeedSalesUnitSeedData.GetFeedSalesUnits());
        }

        public static async Task SeedFeedSalesUnitAsync(CoreDbContext context, List<FeedSalesUnit> feedSalesUnit)
        {
            // Check if there are any existing students
            if (!context.FeedSalesUnits.Any())
            {

                // Add initial data
                context.FeedSalesUnits.AddRange(feedSalesUnit);

                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedSuperAdminAsync(CoreDbContext context, List<BaseUser> baseUsers)
        {
            // Check if there are any existing students
            if (!context.Users.Any())
            {

                // Add initial data
                context.Users.AddRange(baseUsers);

                await context.SaveChangesAsync();
            }
        }
        
    }
}
