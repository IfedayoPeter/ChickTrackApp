

namespace BaseClassLibrary.Repositories.DatabaseSeeder
{
    internal class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Intialize()
        {
            var patientCategories = SuperAdminSeedData.GetSuperAdmins();

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(patientCategories);
            }
            _context.SaveChanges();

        }
    }
}
