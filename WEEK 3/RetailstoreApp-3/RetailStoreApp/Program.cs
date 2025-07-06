using System;
using Microsoft.Data.SqlClient;
using RetailStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RetailStoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            PrintHeader("EF Core CLI - Retail Store Setup");

            try
            {
                using (var context = new AppDbContext())
                {
                    Console.WriteLine("✔ Checking database connection...");

                    if (context.Database.CanConnect())
                    {
                        PrintSuccess("Connected to database.");
                    }
                    else
                    {
                        PrintWarning("Could not connect. Applying migrations...");
                        context.Database.Migrate();
                        PrintSuccess("Database created and migrations applied.");
                    }
                }

                Console.WriteLine("\n📊 Verifying tables...");

                bool categoriesExist = TableExists("Categories");
                bool productsExist = TableExists("Products");

                Console.WriteLine($"✔ Products Table: {(productsExist ? "✅ Exists" : "❌ Missing")}");
                Console.WriteLine($"✔ Categories Table: {(categoriesExist ? "✅ Exists" : "❌ Missing")}");

                Console.WriteLine();
                PrintSuccess("✔ Setup complete. You can now use your database.");
            }
            catch (Exception ex)
            {
                PrintError("❌ An error occurred:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static bool TableExists(string tableName)
        {
            using (var connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=RetailStoreDB;Trusted_Connection=True;"))
            {
                connection.Open();
                var command = new SqlCommand(@"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = @table", connection);
                command.Parameters.AddWithValue("@table", tableName);

                var count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        static void PrintHeader(string message)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"🔧 {message}");
            Console.WriteLine(new string('-', 50));
        }

        static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
