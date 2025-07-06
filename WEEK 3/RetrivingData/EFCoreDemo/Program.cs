using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Models;

class Program
{
    static async Task Main()
    {
        using var context = new AppDbContext();

        // Seed sample data if DB is empty
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new() { Name = "iPhone 15 Pro", Price = 145000 },
                new() { Name = "Samsung TV", Price = 60000 },
                new() { Name = "Bluetooth Mouse", Price = 1500 },
                new() { Name = "Laptop Stand", Price = 900 },
                new() { Name = "Rice Bag 10kg", Price = 800 }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
            Console.WriteLine("✅ Sample data seeded.\n");
        }

        // Step 1: Get all products
        var allProducts = await context.Products.ToListAsync();
        Console.WriteLine("📦 All Products:");
        foreach (var p in allProducts)
            Console.WriteLine($" - {p.Name,-20} ₹{p.Price}");

        // Step 2: Find by ID
        var product = await context.Products.FindAsync(1);
        Console.WriteLine($"\n🔍 Product with ID 1: {(product != null ? product.Name + " - ₹" + product.Price : "Not Found")}");

        // Step 3: First expensive product
        var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 50000);
        Console.WriteLine($"\n💰 First Expensive Product (> ₹50,000): {(expensive != null ? expensive.Name + " - ₹" + expensive.Price : "None Found")}");
    }
}
