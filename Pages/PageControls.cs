using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public class PageControls
    {
        //Alla sido val
        //Vi kan kolla om input var "HOme" istället för 'H' på olika sidor
       public enum PageOption
        {
            Home,
            AddToBasket,
            CustomerLogin,
            CustomerPage,
            CustomerLogout,
            CreateAccount,
            CustomerShippingInfo,
            Checkout,
            BuyCheckout,
            AdjustCheckout,
            AdminLogin,
            AdminCreateComponent,
            AdminCreateCategory,
            AdminCreateStoreProduct,
            AdminEdit,
            AdminPage,
            Search,
            Browse,
            Exit
        }
        //Ett page command, en enum och en beskrivning som används vid printing av valen
        public record PageCommand(PageOption PageCommandOptionInteraction, string CommandDescription);
        
        //Alla fina kontroller
        //Ger giltiga kommandon och beskrivningar

        public static readonly PageCommand HomeCommand =
           new(PageOption.Home, "Go to home page");

        public static readonly PageCommand CustomerShippingInfo =
        new(PageOption.CustomerShippingInfo, "To handle shipping info");

        public static readonly PageCommand BrowseCommand =
            new(PageOption.Browse, "Browse products");

        public static readonly PageCommand ExitCommand =
            new(PageOption.Exit, "Exit application");

        public static readonly PageCommand CustomerLogin =
           new(PageOption.CustomerLogin, "Login as Customer");

        public static readonly PageCommand Admin =
            new(PageOption.AdminPage, "Login to Admin");

        public static readonly PageCommand AdminCreateProduct =
           new(PageOption.AdminCreateComponent, "To register a new product");

        public static readonly PageCommand AdminCreateCategory =
           new(PageOption.AdminCreateCategory, "To register a component specification");

        public static readonly PageCommand AdminEdit = new(PageOption.AdminEdit, "To edit a existing product");

        public static readonly PageCommand Search = new(PageOption.Search, "To search for a product");

        public static readonly PageCommand CustomerHomePage = new(PageOption.CustomerPage, "To view account");

        public static readonly PageCommand AdminCreateStoreProduct = new(PageOption.AdminCreateStoreProduct, "To create a new store product");

        public static readonly PageCommand CustomerLogout = new(PageOption.CustomerLogout, "To log out (if logged in)");

        public static readonly PageCommand Checkout = new(PageOption.Checkout, "To checkout and buy");

        public static readonly PageCommand BuyCheckout = new(PageOption.BuyCheckout, "To buy");
        public static readonly PageCommand CheckoutAdjust = new(PageOption.AdjustCheckout, "To adjust something in the basket");
        public static readonly PageCommand CreateAccount = new(PageOption.CreateAccount, "To create a customer account");
        public static readonly PageCommand AddToBasket = new(PageOption.AddToBasket, "To start adding something to your basket");
    }
}
