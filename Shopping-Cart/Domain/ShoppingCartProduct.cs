namespace Shopping_Cart.Domain
{
    public class ShoppingCartProduct
    {
        public Product Product { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
