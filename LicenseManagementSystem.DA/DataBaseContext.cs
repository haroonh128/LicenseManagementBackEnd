using Microsoft.EntityFrameworkCore;

namespace LicenseManagementSystem.DA
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<License> licenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, 
                    UserName = "admin",
                    Email = "admin@example.com",
                    Password = "Admin@123", 
                    Role = "admin",
                    FirstName = "Admin",
                    LastName = "User"
                },
                new User
                {
                    Id = 2,
                    UserName = "user",
                    Email = "user@example.com",
                    Password = "User@123", 
                    Role = "user",
                    FirstName = "Regular",
                    LastName = "User"
                }
            );
        }
    }
}
