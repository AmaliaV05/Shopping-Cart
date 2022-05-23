using System.Collections.Generic;

namespace Shopping_Cart.Interfaces
{
    public interface IShoppingCartRepository<ShoppingCart>
    {
        /// <summary>
        /// Gets a shopping cart from repository by shopping cart id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        ShoppingCart GetShoppingCart(int cartId);

        /// <summary>
        /// Gets all shopping carts from repository
        /// </summary>
        /// <returns></returns>
        IEnumerable<ShoppingCart> GetShoppingCarts();

        /// <summary>
        /// Adds a shopping cart to repository
        /// </summary>
        /// <param name="cart"></param>
        void AddShoppingCart(ShoppingCart cart);
    }
}
