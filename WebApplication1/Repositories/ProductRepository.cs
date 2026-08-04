using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public static class ProductRepository
    {
        public static List<Product> Products = new()
        {
            new Product
            {
                Id = 1,
                Name = "RTX 5070 Graphics Card",
                Price = 35000,
                StockQuantity = 5
            },
            new Product
            {
                Id = 2,
                Name = "Ryzen 7 7800X3D",
                Price = 22000,
                StockQuantity = 8
            },
            new Product
            {
                Id = 3,
                Name = "1TB NVMe SSD",
                Price = 4500,
                StockQuantity = 12
            },
            new Product
            {
                Id = 4,
                Name = "32GB DDR5 RAM",
                Price = 6500,
                StockQuantity = 10
            },
            new Product
            {
                Id = 5,
                Name = "Mechanical Keyboard",
                Price = 3200,
                StockQuantity = 20
            },
            new Product
            {
                Id = 6,
                Name = "Gaming Mouse",
                Price = 1800,
                StockQuantity = 15
            },
            new Product
            {
                Id = 7,
                Name = "750W Power Supply",
                Price = 5000,
                StockQuantity = 7
            },
            new Product
            {
                Id = 8,
                Name = "Mid-Tower PC Case",
                Price = 4000,
                StockQuantity = 0
            }
        };
    }
}