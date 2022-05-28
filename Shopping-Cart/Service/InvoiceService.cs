using Shopping_Cart.Domain;
using Shopping_Cart.Domain.Enum.Utils;
using Shopping_Cart.Helpers;
using Shopping_Cart.Interfaces;

namespace Shopping_Cart.Service
{
    public class InvoiceService : IInvoiceService<Invoice>
    {
        private readonly IShoppingCartRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IInvoiceRepository<Invoice> _invoiceRepository;

        public InvoiceService(IShoppingCartRepository<ShoppingCart> shoppingCartRepository, IInvoiceRepository<Invoice> invoiceRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _invoiceRepository = invoiceRepository;
        }

        /// <summary>
        /// Creates an invoice for an existent shopping cart:
        ///     <list type="bullet">
        ///         <item><description>Subtotal is calculated as sum of products between the quantity and the price of each item</description></item>
        ///         <item><description>Shipping fees apply based on the country where the item is shipped from and the shipping rate is per 0.1kg</description></item>
        ///     </list>
        /// VAT and discounts are also applied to the total.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public Invoice AddInvoice(int cartId)
        {
            ShoppingCart cart = _shoppingCartRepository.GetShoppingCart(cartId);
            decimal subtotal = 0;
            decimal shippingCost = 0;
            foreach (ShoppingCartProduct cartProduct in cart.ShoppingCartProducts)
            {
                subtotal += cartProduct.Quantity * cartProduct.Product.Price;
                shippingCost += TruncateDecimals.Truncate(cartProduct.Quantity * cartProduct.Product.Weight * cartProduct.Product.ShippingRate.Rate / 0.1m, 2);
            }
            decimal total = subtotal + shippingCost;
            Invoice invoice = new()
            {
                SubTotal = subtotal,
                ShippingCost = shippingCost,
                Total = total,
                ShoppingCart = cart
            };
            _invoiceRepository.AddInvoice(invoice);
            invoice = ApplyVat(invoice.Id);
            invoice = SpecialOffers(invoice.Id);
            return invoice;
        }

        /// <summary>
        /// Applies discounts in according with the following offers: 
        ///     <list type="bullet">
        ///         <item><description>"Keyboards are 10% off"</description></item>
        ///         <item><description>"Buy 2 Monitors and get a desk lamp at half price"</description></item>
        ///         <item><description>"Buy any 2 items or more and get a maximum of $10 off shipping fees"</description></item>
        ///     </list>
        /// These offers can be applied simultaneously and they affect the total of the invoice.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        private Invoice SpecialOffers(int invoiceId)
        {
            Invoice invoice = _invoiceRepository.GetInvoice(invoiceId);
            invoice.Discounts = new();
            Discount discount1 = ApplyDiscountOffer1(invoice);
            Discount discount2 = ApplyDiscountOffer2(invoice);
            Discount discount3 = ApplyDiscountOffer3(invoice);
            if (discount1.Amount.HasValue)
            {
                invoice.Discounts.Add(discount1);
                invoice.Total -= (decimal)discount1.Amount;
            }
            if (discount2.Amount.HasValue)
            {
                invoice.Discounts.Add(discount2);
                invoice.Total -= (decimal)discount2.Amount;
            }
            if (discount3.Amount.HasValue)
            {
                invoice.Discounts.Add(discount3);
                invoice.Total -= (decimal)discount3.Amount;
            }
            return invoice;
        }

        /// <summary>
        /// Applies 10% discount to all keyboards in an invoice and saves it in the repository
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        private Discount ApplyDiscountOffer1(Invoice invoice)
        {
            Discount discount = new();
            bool keyboardExists = invoice.ShoppingCart.Products.Exists(x => x.Name == "Keyboard");
            if (keyboardExists)
            {
                Product keyboard = invoice.ShoppingCart.Products.Find(x => x.Name == "Keyboard");
                int keyboardQuantity = 0;
                foreach (ShoppingCartProduct cartProducts in invoice.ShoppingCart.ShoppingCartProducts)
                {
                    if (cartProducts.Product.Name == "Keyboard")
                    {
                        keyboardQuantity = cartProducts.Quantity;
                    }
                }
                discount = new()
                {
                    Amount = TruncateDecimals.Truncate(keyboard.Price * keyboardQuantity * 0.1m, 2),
                    DiscountType = Discounts.Offer1,
                    Invoice = invoice
                };
                _invoiceRepository.AddDiscount(discount);
            }
            return discount;
        }

        /// <summary>
        /// Applies 50% discount to each desk lamp for each two monitors found in an invoice <br/>
        /// and saves it in the repository
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        private Discount ApplyDiscountOffer2(Invoice invoice)
        {
            Discount discount = new();
            bool monitorsExist = invoice.ShoppingCart.Products.Exists(x => x.Name == "Monitor");
            bool deskLampsExist = invoice.ShoppingCart.Products.Exists(x => x.Name == "Desk Lamp");
            int monitorQuantity = 0;
            int lampQuantity = 0;
            if (monitorsExist && deskLampsExist)
            {
                foreach (ShoppingCartProduct cartProducts in invoice.ShoppingCart.ShoppingCartProducts)
                {
                    if (cartProducts.Product.Name == "Monitor")
                    {
                        monitorQuantity = cartProducts.Quantity;
                    }
                    if (cartProducts.Product.Name == "Desk Lamp")
                    {
                        lampQuantity = cartProducts.Quantity;
                    }
                }
                Product lamp = invoice.ShoppingCart.Products.Find(x => x.Name == "Desk Lamp");
                if (monitorQuantity >= 2 && monitorQuantity / 2 <= lampQuantity)
                {
                    discount = new()
                    {
                        Amount = TruncateDecimals.Truncate(lamp.Price * (monitorQuantity / 2) * 0.5m, 2),
                        DiscountType = Discounts.Offer2,
                        Invoice = invoice
                    };
                    _invoiceRepository.AddDiscount(discount);
                }
                else if (monitorQuantity >= 2 && monitorQuantity / 2 > lampQuantity)
                {
                    discount = new()
                    {
                        Amount = TruncateDecimals.Truncate(lamp.Price * lampQuantity * 0.5m, 2),
                        DiscountType = Discounts.Offer2,
                        Invoice = invoice
                    };
                    _invoiceRepository.AddDiscount(discount);
                }
            }
            return discount;
        }

        /// <summary>
        /// Applies a maximum of 10$ discount to shipping fees if there are at least two items <br/>
        /// in the invoice and saves it in the repository
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        private Discount ApplyDiscountOffer3(Invoice invoice)
        {
            Discount discount = new();
            int quantity = 0;
            foreach (var shoppingProduct in invoice.ShoppingCart.ShoppingCartProducts)
            {
                quantity += shoppingProduct.Quantity;
            }
            if (quantity >= 2)
            {
                if (invoice.ShippingCost >= 10)
                {
                    discount = new()
                    {
                        Amount = 10,
                        DiscountType = Discounts.Offer3,
                        Invoice = invoice
                    };
                    _invoiceRepository.AddDiscount(discount);
                }
                else
                {
                    discount = new()
                    {
                        Amount = invoice.ShippingCost,
                        DiscountType = Discounts.Offer3,
                        Invoice = invoice
                    };
                    _invoiceRepository.AddDiscount(discount);
                }
            }
            return discount;
        }

        /// <summary>
        /// Applies VAT to all products in an invoice. It doesn't apply to the shipping fee, nor discounts.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        private Invoice ApplyVat(int invoiceId)
        {
            Invoice invoice = _invoiceRepository.GetInvoice(invoiceId);
            invoice.Vat = TruncateDecimals.Truncate(invoice.SubTotal * .19m, 2);
            invoice.Total += invoice.Vat;
            return invoice;
        }
    }
}
