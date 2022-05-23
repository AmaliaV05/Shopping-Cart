using System.Collections.Generic;

namespace Shopping_Cart.Interfaces
{
    public interface IProductService<Product>
    {
        /// <summary>
        /// Gets all products from repository
        /// </summary>
        /// <returns></returns>
        List<Product> GetProducts();
    }
}
