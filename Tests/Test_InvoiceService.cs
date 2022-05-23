using NUnit.Framework;
using Shopping_Cart.Domain;
using Shopping_Cart.Domain.Enum.Utils;
using Shopping_Cart.Interfaces;
using Shopping_Cart.Repository;
using Shopping_Cart.Service;
using System.Collections.Generic;

namespace Tests
{
    class Test_InvoiceService
    {
        private static IProductRepository<Product> _productRepository;
        private static IShoppingCartRepository<ShoppingCart> _shoppingCartRepository;
        private static IInvoiceRepository<Invoice> _invoiceRepository;
        private static IShoppingCartService<ShoppingCart> _shoppingCartService;
        private static IInvoiceService<Invoice> _invoiceService;

        [SetUp]
        public void Setup()
        {
            _productRepository = new ProductRepository_Test();
            _shoppingCartRepository = new ShoppingCartRepository();
            _invoiceRepository = new InvoiceRepository();
            _shoppingCartService = new ShoppingCartService(_productRepository, _shoppingCartRepository);
            _invoiceService = new InvoiceService(_shoppingCartRepository, _invoiceRepository);
        }

        /// <summary>
        /// Checks the amount of each detail of a newly created invoice
        /// </summary>
        /// <param name="firstProductId"></param>
        /// <param name="productIds"></param>
        /// <param name="subtotal"></param>
        /// <param name="shipping"></param>
        /// <param name="vat"></param>
        /// <param name="discount1"></param>
        /// <param name="discount2"></param>
        /// <param name="discount3"></param>
        /// <param name="total"></param>
        [Test]
        [TestCaseSource(nameof(InvoiceDataGenerator))]
        public void TestAddInvoice(int firstProductId, int[] productIds, decimal subtotal, decimal shipping, 
            decimal vat, decimal discount1, decimal discount2, decimal discount3, decimal total)
        {
            ShoppingCart cart = _shoppingCartService.AddShoppingCart(firstProductId);
            foreach (int id in productIds)
            {
                cart = _shoppingCartService.AddItemToShoppingCart(cart.Id, id);
            }
            Invoice invoice = _invoiceService.AddInvoice(cart.Id);
            Assert.AreEqual(subtotal, invoice.SubTotal);
            Assert.AreEqual(shipping, invoice.ShippingCost);
            Assert.AreEqual(vat, invoice.Vat);
            if (invoice.Discounts.Exists(x => x.DiscountType == Discounts.Offer1))
            {
                Assert.AreEqual(discount1, invoice.Discounts.Find(x => x.DiscountType == Discounts.Offer1).Amount);
            }
            if (invoice.Discounts.Exists(x => x.DiscountType == Discounts.Offer2))
            {
                Assert.AreEqual(discount2, invoice.Discounts.Find(x => x.DiscountType == Discounts.Offer2).Amount);
            }
            if (invoice.Discounts.Exists(x => x.DiscountType == Discounts.Offer3))
            {
                Assert.AreEqual(discount3, invoice.Discounts.Find(x => x.DiscountType == Discounts.Offer3).Amount);
            }
            Assert.AreEqual(total, invoice.Total);
            Assert.Pass("New invoice was created");
        }

        /// <summary>
        /// Invoice data: first two parameters of 'TestCaseData' refer to product ids and the rest to invoice details
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<TestCaseData> InvoiceDataGenerator()
        {
            int[] productIds1 = { 3, 3 };
            yield return new TestCaseData(2, productIds1, 370.97m, 128m, 70.48m, 4.09m, null, 10m, 555.36m);

            int[] productIds2 = { 4 };
            yield return new TestCaseData(1, productIds2, 95.98m, 4m, 18.23m, null, null, 4m, 114.21m)
                .SetName("TestAddInvoice_SecondTest");
        }
    }
}
