using HardwareStore.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInfo> UsersInfo { get; set; }


    }
}
