using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Models;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     optionsBuilder.UseSqlServer(
    @"Server=(localdb)\MSSQLLocalDB;Database=EFCoreLabDb;Trusted_Connection=True;Encrypt=False;");
    }
}
