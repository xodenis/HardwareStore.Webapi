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

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Favorites> Favorites { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Subcategory> Subcategories { get; set; }

        public DbSet<OrdersProducts> OrdersProducts { get; set; }

        public DbSet<CartProducts> CartProducts { get; set; }

        public DbSet<FavoritesProducts> FavoritesProducts { get; set; }
    }
}
