using System.Collections.Generic;

namespace Shopping_Cart.Domain
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public List<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public Invoice Invoice { get; set; }
    }
}
