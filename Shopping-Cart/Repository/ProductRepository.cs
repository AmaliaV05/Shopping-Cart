using Shopping_Cart.Domain;
using Shopping_Cart.Domain.Enum.Utils;
using Shopping_Cart.Interfaces;
using System.Collections.Generic;

namespace Shopping_Cart.Repository
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly List<Product> products = new();
        private readonly List<ShippingRate> shippingRates = new();

        public ProductRepository()
        {
            shippingRates.AddRange(new ShippingRate[]
            {
                new ShippingRate { Id = 1, Country = ShippingCountry.RO, Rate = 1 },
                new ShippingRate { Id = 2, Country = ShippingCountry.UK, Rate = 2 },
                new ShippingRate { Id = 3, Country = ShippingCountry.US, Rate = 3 }
            });
            products.AddRange(new Product[]
            {
                new Product { Id = 1, Name = "Mouse", Price = 10.99m, Weight = 0.2m, ShippingRate = shippingRates[0] },
                new Product { Id = 2, Name = "Keyboard", Price = 40.99m, Weight = 0.7m, ShippingRate = shippingRates[1] },
                new Product { Id = 3, Name = "Monitor", Price = 164.99m, Weight = 1.9m, ShippingRate = shippingRates[2] },
                new Product { Id = 4, Name = "Webcam", Price = 84.99m, Weight = 0.2m, ShippingRate = shippingRates[0] },
                new Product { Id = 5, Name = "Headphones", Price = 59.99m, Weight = 0.6m, ShippingRate = shippingRates[2] },
                new Product { Id = 6, Name = "Desk Lamp", Price = 89.99m, Weight = 1.3m, ShippingRate = shippingRates[1] }
            });
        }

        /// <summary>
        /// Gets a product from repository by product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProduct(int productId)
        {
            return products[productId - 1];
        }

        /// <summary>
        /// Gets all product from repository
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            return products;
        }
    }
}
