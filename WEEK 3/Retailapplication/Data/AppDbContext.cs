using Microsoft.EntityFrameworkCore;
using RetailStoreApp.Models;

namespace RetailStoreApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}