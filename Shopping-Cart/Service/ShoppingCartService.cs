using Shopping_Cart.Domain;
using Shopping_Cart.Interfaces;
using System.Collections.Generic;

namespace Shopping_Cart.Service
{
    public class ShoppingCartService : IShoppingCartService<ShoppingCart>
    {
        private readonly IProductRepository<Product> _productRepository;
        private readonly IShoppingCartRepository<ShoppingCart> _shoppingCartRepository;

        public ShoppingCartService(IProductRepository<Product> productRepository, IShoppingCartRepository<ShoppingCart> shoppingCartRepository)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        /// <summary>
        /// Gets shopping cart from repository by shopping cart id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public ShoppingCart GetShoppingCart(int cartId)
        {
            return _shoppingCartRepository.GetShoppingCart(cartId);
        }


        /// <summary>
        /// Creates a new shopping cart by adding one product of choice
        /// and adding it to the repository
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ShoppingCart AddShoppingCart(int productId)
        {
            Product product = _productRepository.GetProduct(productId);
            List<Product> products = new();
            products.Add(product);
            ShoppingCartProduct shoppingCartProduct = new()
            {
                Product = product,
                Quantity = 1
            };
            List<ShoppingCartProduct> shoppingCartProducts = new();
            shoppingCartProducts.Add(shoppingCartProduct);
            ShoppingCart cart = new()
            {
                Products = products,
                ShoppingCartProducts = shoppingCartProducts
            };
            _shoppingCartRepository.AddShoppingCart(cart);
            return cart;
        }

        /// <summary>
        /// Adds a product to an existing shopping cart and saves it in the repository
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ShoppingCart AddItemToShoppingCart(int cartId, int productId)
        {
            ShoppingCart cart = _shoppingCartRepository.GetShoppingCart(cartId);
            Product product = _productRepository.GetProduct(productId);
            bool productExists = cart.ShoppingCartProducts.Exists(x => x.Product.Id == productId);
            if (productExists)
            {
                cart.ShoppingCartProducts.Find(x => x.Product.Id == productId).Quantity++;
            }
            else
            {
                ShoppingCartProduct shoppingCartProduct = new()
                {
                    Product = product,
                    Quantity = 1
                };
                cart.ShoppingCartProducts.Add(shoppingCartProduct);
                cart.Products.Add(product);
            }
            return cart;
        }
    }
}
