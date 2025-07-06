using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RetailInventory.Data;
using RetailInventory.Models;

namespace RetailInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new RetailDbContext();
            context.Database.EnsureCreated();

            // ✅ Seed only if database is empty
            if (!context.Products.Any())
            {
                var electronics = new Category { Name = "Electronics" };
                var clothing = new Category { Name = "Clothing" };
                var groceries = new Category { Name = "Groceries" };

                context.Categories.AddRange(electronics, clothing, groceries);
                context.SaveChanges();

                var products = new[]
                {
                    new Product { Name = "Laptop", Stock = 10, CategoryId = electronics.Id },
                    new Product { Name = "Smartphone", Stock = 15, CategoryId = electronics.Id },
                    new Product { Name = "Headphones", Stock = 30, CategoryId = electronics.Id },

                    new Product { Name = "T-Shirt", Stock = 50, CategoryId = clothing.Id },
                    new Product { Name = "Jeans", Stock = 25, CategoryId = clothing.Id },
                    new Product { Name = "Jacket", Stock = 10, CategoryId = clothing.Id },

                    new Product { Name = "Rice", Stock = 100, CategoryId = groceries.Id },
                    new Product { Name = "Milk", Stock = 40, CategoryId = groceries.Id },
                    new Product { Name = "Eggs", Stock = 60, CategoryId = groceries.Id }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // ✅ Display products
            Console.WriteLine("\n📦 Inventory List:");
            Console.WriteLine(new string('-', 50));
            foreach (var product in context.Products.Include(p => p.Category))
            {
                Console.WriteLine($"Product: {product.Name,-15} | Stock: {product.Stock,3} | Category: {product.Category.Name}");
            }
            Console.WriteLine(new string('-', 50));
        }
    }
}
