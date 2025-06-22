using System;
using System.Linq;

namespace ECommerceSearch
{
    public class Product
    {
        public int ProdId { get; }
        public string ProdName { get; }
        public string Category { get; }
        public Product(int id, string name, string category)
        {
            ProdId = id;
            ProdName = name;
            Category = category;
        }
        public override string ToString() =>
            $"[{ProdId}] {ProdName} ({Category})";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var products = new[]
            {
                new Product(101, "T-shirt",        "Clothing"),
                new Product(205, "Sneakers",       "Footwear"),
                new Product(150, "Jeans",          "Clothing"),
                new Product(310, "Backpack",       "Accessories"),
                new Product(120, "Cap",            "Accessories"),
                new Product(180, "Laptop",         "Electronics"),
                new Product(420, "Smartphone",     "Electronics"),
                new Product(390, "Watch",          "Accessories"),
                new Product(230, "Sandals",        "Footwear"),
                new Product(340, "Jacket",         "Clothing"),
                new Product(360, "Bluetooth Speaker", "Electronics"),
                new Product(370, "Wallet",         "Accessories"),
                new Product(410, "Socks",          "Clothing"),
                new Product(275, "Curtains",       "Home"),
                new Product(290, "Lamp",           "Home"),
                new Product(450, "Mixer Grinder",  "Electronics"),
                new Product(460, "Headphones",     "Electronics"),
                new Product(470, "Bed Sheet",      "Home"),
                new Product(480, "Coffee Mug",     "Home"),
                new Product(490, "Keyboard",       "Electronics")
            };

            int idToFind = 460
            ;

            Console.WriteLine("=== Linear Search ===");
            int idxLinear = LinearSearch(products, idToFind);
            if (idxLinear >= 0)
                Console.WriteLine($"Found {products[idxLinear]} at index {idxLinear}");
            else
                Console.WriteLine($"Product {idToFind} not found");

            Console.WriteLine("\n=== Binary Search ===");
            var sorted = products.OrderBy(p => p.ProdId).ToArray();
            Console.WriteLine("Sorted Product IDs: " + string.Join(", ", sorted.Select(p => p.ProdId)));

            int idxBinary = BinarySearch(sorted, idToFind);
            if (idxBinary >= 0)
                Console.WriteLine($"Found {sorted[idxBinary]} at index {idxBinary}");
            else
                Console.WriteLine($"Product {idToFind} not found");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        public static int LinearSearch(Product[] list, int targetId)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i].ProdId == targetId)
                    return i;
            return -1;
        }
        public static int BinarySearch(Product[] sortedList, int targetId)
        {
            int left = 0, right = sortedList.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int midId = sortedList[mid].ProdId;

                if (midId == targetId)
                    return mid;
                else if (midId < targetId)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return -1;
        }
    }
}
