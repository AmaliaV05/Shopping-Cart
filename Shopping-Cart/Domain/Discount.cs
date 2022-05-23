using Shopping_Cart.Domain.Enum.Utils;

namespace Shopping_Cart.Domain
{
    public class Discount
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public Discounts DiscountType { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
