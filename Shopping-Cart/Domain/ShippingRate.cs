using Shopping_Cart.Domain.Enum.Utils;
using System.Collections.Generic;

namespace Shopping_Cart.Domain
{
    public class ShippingRate
    {
        public int Id { get; set; }
        public ShippingCountry Country { get; set; }
        public decimal Rate { get; set; }
        public List<Product> Products { get; set; }
    }
}
