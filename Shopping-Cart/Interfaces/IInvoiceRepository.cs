using Shopping_Cart.Domain;
using System.Collections.Generic;

namespace Shopping_Cart.Interfaces
{
    public interface IInvoiceRepository<Invoice>
    {
        /// <summary>
        /// Gets an invoice from repository by invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        Invoice GetInvoice(int invoiceId);

        /// <summary>
        /// Gets all invoices from repository
        /// </summary>
        /// <returns></returns>
        IEnumerable<Invoice> GetInvoices();

        /// <summary>
        /// Adds an invoice tothe repository
        /// </summary>
        /// <param name="invoice"></param>
        void AddInvoice(Invoice invoice);

        /// <summary>
        /// Adds a discount to the repository
        /// </summary>
        /// <param name="discount"></param>
        void AddDiscount(Discount discount);
    }
}
