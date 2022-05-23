using Shopping_Cart.Domain;
using Shopping_Cart.Interfaces;
using Shopping_Cart.Repository;
using Shopping_Cart.Service;
using Shopping_Cart.UI;

namespace Shopping_Cart
{
    class Program
    {
        static void Main()
        {
            IProductRepository<Product> productRepository = new ProductRepository();
            IShoppingCartRepository<ShoppingCart> shoppingCartRepository = new ShoppingCartRepository();
            IInvoiceRepository<Invoice> invoiceRepository = new InvoiceRepository();

            IProductService<Product> productService = new ProductService(productRepository);
            IShoppingCartService<ShoppingCart> shoppingCartService = new ShoppingCartService(productRepository, shoppingCartRepository);
            IInvoiceService<Invoice> invoiceService = new InvoiceService(shoppingCartRepository, invoiceRepository);
            
            UiService uiService = new(productService, shoppingCartService, invoiceService);
            uiService.Run();
        }
    }
}
