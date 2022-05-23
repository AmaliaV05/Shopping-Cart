using Shopping_Cart.Domain;
using Shopping_Cart.Interfaces;
using System.Collections.Generic;

namespace Shopping_Cart.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository<ShoppingCart>
    {
        private readonly List<ShoppingCart> shoppingCarts;

        public ShoppingCartRepository()
        {
            this.shoppingCarts = new();
        }

        /// <summary>
        /// Gets a shopping cart from repository by shopping cart id 
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public ShoppingCart GetShoppingCart(int cartId)
        {
            return shoppingCarts[cartId - 1];
        }

        /// <summary>
        /// Gets all shopping carts from repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShoppingCart> GetShoppingCarts()
        {
            return shoppingCarts;
        }

        /// <summary>
        /// Adds a shopping cart to repository
        /// </summary>
        /// <param name="cart"></param>
        public void AddShoppingCart(ShoppingCart cart)
        {
            shoppingCarts.Add(cart);
            cart.Id = shoppingCarts.Count;
        }
    }
}
