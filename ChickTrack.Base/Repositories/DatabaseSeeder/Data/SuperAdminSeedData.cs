namespace BaseClassLibrary.Repositories.DatabaseSeeder.Data
{
    internal class SuperAdminSeedData
    {
        public static List<BaseUser> GetSuperAdmins()
        {
            var passwordHasher = new PasswordHasher<BaseUser>();
            return new List<BaseUser>
                {
                    new BaseUser
                    {
                        FullName = "superadmin",
                        UserName = "SUPERADMIN",
                        Email = "admin@gmail.com",
                        PhoneNumber = "1234567890",
                        PasswordHash = passwordHasher.HashPassword(null, "Admin@123"),
                        CreatedBy = "system",
                        CreatedOn = DateTime.UtcNow,
                        LastModifiedBy = "system",
                        LastModifiedOn = DateTime.UtcNow,
                    }
                };
        }
    }
}
