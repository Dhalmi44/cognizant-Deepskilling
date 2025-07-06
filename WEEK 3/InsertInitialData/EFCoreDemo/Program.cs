using EFCoreDemo;
using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("🔧 Starting EF Core Product Demo...\n");

using var context = new AppDbContext();

// Apply migrations
Console.WriteLine("🔄 Applying pending migrations...");
await context.Database.MigrateAsync();
Console.WriteLine("✅ Database is up to date.\n");

// Seed Data
Console.WriteLine("📝 Checking for existing data...");
if (!await context.Categories.AnyAsync())
{
    Console.WriteLine("📦 Seeding initial data into the database...");

    var electronics = new Category { Name = "Electronics" };
    var groceries = new Category { Name = "Groceries" };

    var product1 = new Product { Name = "💻 Laptop", Price = 75000, Category = electronics };
    var product2 = new Product { Name = "🏷️ Rice Bag", Price = 1200, Category = groceries };

    await context.Categories.AddRangeAsync(electronics, groceries);
    await context.Products.AddRangeAsync(product1, product2);
    await context.SaveChangesAsync();

    Console.WriteLine("✅ Data insertion completed.\n");
}
else
{
    Console.WriteLine("⚠️ Data already exists.\n");
}

// Show Products
Console.WriteLine("📋 Listing all products in the database:");
var products = await context.Products.Include(p => p.Category).ToListAsync();
foreach (var product in products)
{
    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
    Console.WriteLine($"📦 Product: {product.Name}");
    Console.WriteLine($"📂 Category: {product.Category.Name}");
    Console.WriteLine($"💰 Price: ₹{product.Price:N2}");
}
Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

Console.WriteLine("\n🎉 Done. Application finished successfully.");
