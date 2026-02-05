using ComputerStoreApplication.Account;
using ComputerStoreApplication.Crud_Related;
using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComputerStoreApplication.Pages.PageControls;

namespace ComputerStoreApplication.Pages
{
    internal class CheckoutPage : IPage
    {
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        public int? AdminId { get; set; }
        CustomerAccount? CurrentCustomer { get; set; }
        public int? CurrentCustomerId { get; set; }
        private List<BasketProduct> basketProducts = new();
        public async Task<IPage?> HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this; //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Bokstaven N är skapa ny produkt, vi laddar om samma sida, fast kallar en metod innan
                case PageControls.PageOption.BuyCheckout:
                    if (applicationLogic.IsLoggedInAsCustomer)
                    {
                        applicationLogic.HandleCustomerPurchase(CurrentCustomerId.Value);
                        applicationLogic.RefreshCurrentCustomerBasket(CurrentCustomer);
                        Console.ReadLine();
                    }
                        return this;
                case PageControls.PageOption.AdjustCheckout:
                    if (applicationLogic.IsLoggedInAsCustomer)
                    {
                        CrudCreatorHelper.AdjustBasketItems(CurrentCustomer, applicationLogic);
                        applicationLogic.RefreshCurrentCustomerBasket(CurrentCustomer);
                        //applicationLogic.VerifyStoreItems();
                        //applicationLogic.VerifyBasketItems(CurrentCustomerId);
                        //applicationLogic.ComputerPartShopDB.SaveChanges();
                        
                    }
                    return this;
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerPage:
                    return new CustomerPage();
                case PageControls.PageOption.Browse:
                    return new BrowseProducts();
            }
            ;
            return this;

        }

        public void Load(ApplicationManager appLol)
        {
            appLol.VerifyStoreItems();
            if (!appLol.IsLoggedInAsCustomer)
            {
                CurrentCustomerId = null;
                CurrentCustomer = null;
                Console.WriteLine("Can't access without being logged in");
                Console.ReadLine();
                appLol.CurrentPage = new HomePage();
                
            }
            //om inloggad, hämta nnuvarande kund
            CurrentCustomerId = appLol.CustomerId;
            if (CurrentCustomerId == null)
            {
                return;
            }
            basketProducts = appLol.CurrentBasket.ToList();
            CurrentCustomer = appLol.GetCustomerInfo(CurrentCustomerId.Value);
            //RefreshBasketInfo(appLol);
          
        }
        public void RefreshBasketInfo(ApplicationManager appLol)
        {
            appLol.RefreshCurrentCustomerBasket(CurrentCustomer);

            appLol.VerifyBasketItems(CurrentCustomerId);

        }

        public void RenderPage(ApplicationManager applicationLogic)
        {
            ConsoleHelper.ResetConsole();
            Graphics.PageBanners.DrawCheckoutPage();
            SetPageCommands();
            DrawAccountProfile(applicationLogic);
            if(basketProducts.Count > 0)
            {
                Console.WriteLine("Different products in basket: " + basketProducts.Count + ", with " + basketProducts.Sum(x => x.Quantity).ToString() + " total amount of products in the basket");

                foreach (var product in basketProducts)
                {
                    Console.WriteLine($"Id {product.Id} Name: {product.ComputerPart.Name} Quantity: {product.Quantity} Price: {product.ComputerPart.Price}");
                }

                Console.WriteLine("\nExpected cost (without shipping) ~ " + basketProducts.Sum(x => x.ComputerPart.Price * x.Quantity).ToString() + "€");
            }
            else
            {
                Console.WriteLine("You'll se info about your basket items here, when you fill it upp");
            }
          
        }
        public void DrawAccountProfile(ApplicationManager applicationLogic)
        {
            List<string> accountInfo = new List<string>();
            accountInfo = PageAccount.ReturnCustomerProfileAccountString(applicationLogic);
            PageAccount.DrawAccountGraphic(accountInfo, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 10);
        }

        public void SetPageCommands()
        {
            //Specific commands per sida
            //Sparar kommandon till tangenter på sidor
            PageCommands = new Dictionary<ConsoleKey, PageControls.PageCommand>
            {
                { ConsoleKey.H, PageControls.HomeCommand },
                {ConsoleKey.C, PageControls. CustomerHomePage},
                {ConsoleKey.B, PageControls.BrowseCommand},
                {ConsoleKey.F, PageControls.Search},
                {ConsoleKey.K, PageControls.BuyCheckout },
                {ConsoleKey.J, PageControls.CheckoutAdjust }
              
            };
            //hitta beskrivningarna
            var pageOptions = PageCommands.Select(c => $"[{c.Key}] {c.Value.CommandDescription}").ToList();

            //Boom, rita dem
            if (pageOptions.Any())
            {
                Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkCyan);
            }
        }
    }
}
