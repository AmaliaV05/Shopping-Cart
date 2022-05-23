using System.ComponentModel;

namespace Shopping_Cart.Domain.Enum.Utils
{
    public enum Discounts
    {
        [Description("Keyboards are 10% off")]
        Offer1,
        [Description("Buy 2 Monitors and get a desk lamp at half price")]
        Offer2,
        [Description("Buy any 2 items or more and get a maximum of $10 off shipping fees")]
        Offer3
    }
}
