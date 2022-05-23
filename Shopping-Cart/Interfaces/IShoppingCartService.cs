namespace Shopping_Cart.Interfaces
{
    public interface IShoppingCartService<ShoppingCart>
    {
        /// <summary>
        /// Gets a shopping cart from repository by cart id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        ShoppingCart GetShoppingCart(int cartId);

        /// <summary>
        /// Creates a new shopping cart and saves it to repository
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ShoppingCart AddShoppingCart(int productId);

        /// <summary>
        /// Adds new product to an existent shopping cart and saves it to the repository
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        ShoppingCart AddItemToShoppingCart(int cartId, int productId);        
    }
}
