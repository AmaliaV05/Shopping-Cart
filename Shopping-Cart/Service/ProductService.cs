using Shopping_Cart.Domain;
using Shopping_Cart.Interfaces;
using System.Collections.Generic;

namespace Shopping_Cart.Service
{
    public class ProductService : IProductService<Product>
    {
        private readonly IProductRepository<Product> _productRepository;

        public ProductService(IProductRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Gets all products from repository
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}
