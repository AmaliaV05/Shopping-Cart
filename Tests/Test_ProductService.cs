using NUnit.Framework;
using Shopping_Cart.Domain;
using Shopping_Cart.Interfaces;
using Shopping_Cart.Service;

namespace Tests
{
    class Test_ProductService
    {
        private static IProductRepository<Product> _productRepository;

        [SetUp]
        public void Setup()
        {
            _productRepository = new ProductRepository_Test();
        }

        /// <summary>
        /// Checks the number of products in the catalog
        /// </summary>
        [Test]
        public void TestGetProducts()
        {
            IProductService<Product> productService = new ProductService(_productRepository);
            Assert.AreEqual(7, productService.GetProducts().Count);
            Assert.Pass("All products are visible");
        }
    }
}