namespace Shopping_Cart.Interfaces
{
    public interface IInvoiceService<Invoice>
    {
        /// <summary>
        /// Creates an invoice for an existent shopping cart, where shipping fees apply <br/>
        /// based on the country where the item is shipped from, a 19% VAT is applied <br/>
        /// to all products and discounts also.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Invoice AddInvoice(int cartId);
    }
}
