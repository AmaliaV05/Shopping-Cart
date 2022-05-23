using Shopping_Cart.Domain;
using Shopping_Cart.Domain.Enum.Utils;
using Shopping_Cart.Interfaces;
using System;
using System.Collections.Generic;

namespace Shopping_Cart.UI
{
    public class UiService
    {
        private readonly IProductService<Product> _productService;
        private readonly IShoppingCartService<ShoppingCart> _shoppingCartService;
        private readonly IInvoiceService<Invoice> _invoiceService;

        public UiService(IProductService<Product> productService, IShoppingCartService<ShoppingCart> shoppingCartService, IInvoiceService<Invoice> invoiceService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _invoiceService = invoiceService;
        }

        /// <summary>
        /// Displays the catalog and the product menu. After adding each product to the shopping cart, <br/>
        /// the shopping cart is displayed, and an operation menu appears, with three possible choices: <br/>
        /// ending the shopping session and displaying the invoice, discarding the shopping cart or ending <br/>
        /// the application.
        /// </summary>
        public void Run()
        {
            List<string> products = new(new string[] { "a", "b", "c", "d", "e", "f" });
            List<string> operations = new(new string[] { "add", "fin", "d" });
            bool endApp = false;
            DisplayCatalog();            
            Console.WriteLine("------------------------\n");
            while (!endApp)
            {
                bool endShoppingSession = false;
                while (!endShoppingSession)
                {
                    bool finish = false;
                    string product = ChooseProductMenu(products);
                    ShoppingCart cart = AddShoppingCart(product);
                    DisplayShoppingCart(cart.Id);
                    string operation = ChooseOperationMenu(operations);                    
                    if (operation == "fin")
                    {
                        DisplayInvoice(cart.Id);
                        break;
                    } 
                    else if (operation == "d")
                    {
                        break;
                    }
                    else if (operation == "add")
                    {
                        while (!finish)
                        {
                            product = ChooseProductMenu(products);
                            cart = AddItemToShoppingCart(cart.Id, product);
                            DisplayShoppingCart(cart.Id);
                            operation = ChooseOperationMenu(operations);
                            if (operation == "fin")
                            {
                                DisplayInvoice(cart.Id);
                                endShoppingSession = true;
                                break;
                            }
                            else if (operation == "d")
                            {
                                endShoppingSession = true;
                                break;
                            }
                        }
                    }
                }
                Console.Write("\nPress 'x' and Enter to close the app, or press any other key and Enter to create a new shopping cart: ");
                if (Console.ReadLine() == "x")
                {
                    endApp = true;
                }
                Console.WriteLine("\n");
            }
            return;
        }

        /// <summary>
        /// Displays all catalog items by their names and prices
        /// </summary>
        private void DisplayCatalog()
        {
            IEnumerable<Product> products = _productService.GetProducts();
            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Name} - ${product.Price}");
            }
        }

        /// <summary>
        /// Displays shopping cart with chosen items and their respective quantity
        /// </summary>
        /// <param name="cartId"></param>
        private void DisplayShoppingCart(int cartId)
        {
            ShoppingCart cart = _shoppingCartService.GetShoppingCart(cartId);
            Console.WriteLine("\n------Shopping cart------");
            foreach (ShoppingCartProduct cartProduct in cart.ShoppingCartProducts)
            {
                Console.WriteLine($"{cartProduct.Product.Name} X {cartProduct.Quantity}");
            }
            Console.WriteLine("-------------------------");
        }

        /// <summary>
        /// Creates a new shopping cart after choosing the first item
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private ShoppingCart AddShoppingCart(string product)
        {
            return product switch
            {
                "a" => _shoppingCartService.AddShoppingCart(1),
                "b" => _shoppingCartService.AddShoppingCart(2),
                "c" => _shoppingCartService.AddShoppingCart(3),
                "d" => _shoppingCartService.AddShoppingCart(4),
                "e" => _shoppingCartService.AddShoppingCart(5),
                "f" => _shoppingCartService.AddShoppingCart(6),
                _ => null,
            };
        }

        /// <summary>
        /// Adds the chosen item to the current shopping cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        private ShoppingCart AddItemToShoppingCart(int cartId, string product)
        {
            return product switch
            {
                "a" => _shoppingCartService.AddItemToShoppingCart(cartId, 1),
                "b" => _shoppingCartService.AddItemToShoppingCart(cartId, 2),
                "c" => _shoppingCartService.AddItemToShoppingCart(cartId, 3),
                "d" => _shoppingCartService.AddItemToShoppingCart(cartId, 4),
                "e" => _shoppingCartService.AddItemToShoppingCart(cartId, 5),
                "f" => _shoppingCartService.AddItemToShoppingCart(cartId, 6),
                _ => null,
            };
        }

        /// <summary>
        /// Displays invoice details: subtotal, shipping fees, VAT, discounts (if appliable) and total
        /// </summary>
        /// <param name="cartId"></param>
        private void DisplayInvoice(int cartId)
        {
            Invoice invoice = _invoiceService.AddInvoice(cartId);
            Console.WriteLine("\n------Invoice------");
            Console.WriteLine($"Subtotal: ${invoice.SubTotal}\nShipping: ${invoice.ShippingCost}\nVAT: ${invoice.Vat}");
            if (InvoiceDiscountExists(invoice, Discounts.Offer1) || InvoiceDiscountExists(invoice, Discounts.Offer2) || InvoiceDiscountExists(invoice, Discounts.Offer3))
            {
                Console.WriteLine("\nDiscounts:");
            }            
            if (InvoiceDiscountExists(invoice, Discounts.Offer1))
            {
                Console.WriteLine($"10% off keyboards: -${InvoiceDiscountValue(invoice, Discounts.Offer1)}");
            }
            if (InvoiceDiscountExists(invoice, Discounts.Offer2))
            {
                Console.WriteLine($"50% off desk lamp: -${InvoiceDiscountValue(invoice, Discounts.Offer2)}");
            }
            if (InvoiceDiscountExists(invoice, Discounts.Offer3))
            {
                Console.WriteLine($"$10 off shipping: -${InvoiceDiscountValue(invoice, Discounts.Offer3)}");
            }
            Console.WriteLine($"\nTotal: ${invoice.Total}");
            Console.WriteLine("-------------------");
        }

        /// <summary>
        /// Checks if an invoice has a type of discount
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool InvoiceDiscountExists(Invoice invoice, Discounts type) => invoice.Discounts.Exists(x => x.DiscountType == type);

        /// <summary>
        /// Gets value for a type of discount 
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static decimal? InvoiceDiscountValue(Invoice invoice, Discounts type) => invoice.Discounts.Find(x => x.DiscountType == type).Amount;

        /// <summary>
        /// Display menu to choose products. If the user input is different from expected,
        /// a message will appear and the user can write new input.
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        private static string ChooseProductMenu(List<string> products)
        {
            Console.WriteLine("Choose an item to add to the cart:");
            Console.WriteLine("\ta - Mouse");
            Console.WriteLine("\tb - Keyboard");
            Console.WriteLine("\tc - Monitor");
            Console.WriteLine("\td - Webcam");
            Console.WriteLine("\te - Headphones");
            Console.WriteLine("\tf - Desk Lamp");
            Console.Write("Your option? ");
            string product = Console.ReadLine();
            while (!products.Contains(product))
            {
                Console.Write("Please select an existent product: ");
                product = Console.ReadLine();
            }
            return product;
        }

        /// <summary>
        /// Display operation menu to add new item to the shopping cart, proceed to getting the invoice, <br/>
        /// or discard the shopping cart, without deleting it. If the user input is different from expected, <br/>
        /// a message will appear and the user can write new input.
        /// </summary>
        /// <param name="operations"></param>
        /// <returns></returns>
        private static string ChooseOperationMenu(List<string> operations)
        {
            Console.WriteLine("\tadd - Add another item to cart");
            Console.WriteLine("\tfin - Proceed to next step");
            Console.WriteLine("\td - Discard the cart");
            Console.Write("Your option? ");
            string operation = Console.ReadLine();
            while (!operations.Contains(operation))
            {
                Console.Write("Please select an existent operation: ");
                operation = Console.ReadLine();
            }
            return operation;
        }
    }
}
