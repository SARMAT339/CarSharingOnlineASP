using CarSharing.DB.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarSharing.DB
{
    public class DatabaseContext:IdentityDbContext<UserDB>
    {
        public DbSet<CarDB> CarsDB { get; set; }
        public DbSet<RentDB> RentsDB { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
