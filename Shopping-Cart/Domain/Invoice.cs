using System.Collections.Generic;

namespace Shopping_Cart.Domain
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Vat { get; set; }
        public decimal Total { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public List<Discount> Discounts { get; set; }
    }
}
