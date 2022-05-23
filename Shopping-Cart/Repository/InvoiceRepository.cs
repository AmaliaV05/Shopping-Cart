using Shopping_Cart.Domain;
using Shopping_Cart.Interfaces;
using System.Collections.Generic;

namespace Shopping_Cart.Repository
{
    public class InvoiceRepository : IInvoiceRepository<Invoice>
    {
        private readonly List<Invoice> invoices;
        private readonly List<Discount> discounts;

        public InvoiceRepository()
        {
            invoices = new();
            discounts = new();
        }

        /// <summary>
        /// Gets an invoice from repository by invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public Invoice GetInvoice(int invoiceId)
        {
            return invoices[invoiceId - 1];
        }

        /// <summary>
        /// Gets all invoices from repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Invoice> GetInvoices()
        {
            return invoices;
        }

        /// <summary>
        /// Adds an invoice into the repository
        /// </summary>
        /// <param name="invoice"></param>
        public void AddInvoice(Invoice invoice)
        {
            invoices.Add(invoice);
            invoice.Id = invoices.Count;
        }

        /// <summary>
        /// Adds a discount into the repository
        /// </summary>
        /// <param name="discount"></param>
        public void AddDiscount(Discount discount)
        {
            discounts.Add(discount);
            discount.Id = discounts.Count;
        }
    }
}
