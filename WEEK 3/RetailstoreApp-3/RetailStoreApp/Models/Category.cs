using System.Collections.Generic;

namespace RetailStoreApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
