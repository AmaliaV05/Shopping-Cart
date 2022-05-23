using System.Collections.Generic;

namespace Shopping_Cart.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public ShippingRate ShippingRate { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; }
        public List<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}
