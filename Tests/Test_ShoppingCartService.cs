using NUnit.Framework;
using Shopping_Cart.Domain;
using Shopping_Cart.Interfaces;
using Shopping_Cart.Repository;
using Shopping_Cart.Service;
using System.Collections.Generic;

namespace Tests
{
    class Test_ShoppingCartService
    {
        private static IProductRepository<Product> _productRepository;
        private static IShoppingCartRepository<ShoppingCart> _shoppingCartRepository;
        private static IShoppingCartService<ShoppingCart> _shoppingCartService;

        [SetUp]
        public void Setup()
        {
            _productRepository = new ProductRepository_Test();
            _shoppingCartRepository = new ShoppingCartRepository();
            _shoppingCartService = new ShoppingCartService(_productRepository, _shoppingCartRepository);
        }

        /// <summary>
        /// Checks if a shopping cart containing one product was created
        /// </summary>
        /// <param name="productId"></param>
        [Test]
        [TestCaseSource(nameof(ShoppingCartDataGenerator))]
        public void TestAddShoppingCart(int productId)
        {
            ShoppingCart cart = _shoppingCartService.AddShoppingCart(productId);
            Assert.AreEqual(1, cart.ShoppingCartProducts.Count);
            Assert.AreEqual(1, cart.ShoppingCartProducts[0].Quantity);
            Assert.Pass("New shopping cart was created");
        }

        /// <summary>
        /// Checks the contents of the shopping cart after adding more than one products
        /// </summary>
        /// <param name="firstProductId"></param>
        /// <param name="productIds"></param>
        /// <param name="countDifferentProducts"></param>
        /// <param name="quantity"></param>
        [Test]
        [TestCaseSource(nameof(ShoppingCartAddDataGenerator))]
        public void TestAddItemsToShoppingCart(int firstProductId, int[] productIds, int countDifferentProducts, int quantity)
        {
            ShoppingCart cart = _shoppingCartService.AddShoppingCart(firstProductId);
            foreach (int id in productIds)
            {
                cart = _shoppingCartService.AddItemToShoppingCart(cart.Id, id);
            }
            Assert.AreEqual(countDifferentProducts, cart.ShoppingCartProducts.Count);
            int actualQuantity = 0;
            foreach (var cartProduct in cart.ShoppingCartProducts)
            {
                actualQuantity += cartProduct.Quantity;
            }
            Assert.AreEqual(quantity, actualQuantity);
            Assert.Pass("Shopping cart has two or more items");
        }

        /// <summary>
        /// Product data: the parameter of 'TestCaseData' refers to product id
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<TestCaseData> ShoppingCartDataGenerator()
        {
            yield return new TestCaseData(1);
            yield return new TestCaseData(2)
                .SetName("TestAddShoppingCart_SecondTest");
        }

        /// <summary>
        /// Product and shopping cart data: first two parameters of 'TestCaseData' 
        /// refer to product ids, the third, to number of different products, 
        /// and the last, to the total items in the cart
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<TestCaseData> ShoppingCartAddDataGenerator()
        {
            int[] productIds1 = { 1, 3, 3 };
            yield return new TestCaseData(1, productIds1, 2, 4);

            int[] productIds2 = { 2, 5, 6, 6 };
            yield return new TestCaseData(1, productIds2, 4, 5)
                .SetName("TestAddItemsToShoppingCart_SecondTest");
        }
    }
}
