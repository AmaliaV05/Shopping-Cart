using System.Collections.Generic;

namespace Shopping_Cart.Interfaces
{
    public interface IProductRepository<Product>
    {
        /// <summary>
        /// Gets product from repository by product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product GetProduct(int productId);

        /// <summary>
        /// Gets all product from repository
        /// </summary>
        /// <returns></returns>
        List<Product> GetProducts();
    }
}
