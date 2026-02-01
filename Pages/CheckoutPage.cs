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
        private List<BasketProduct> BasketProducts = new List<BasketProduct>();

        public int? AdminId { get; set; }
        CustomerAccount? CurrentCustomer { get; set; }
        public int? CurrentCustomerId { get; set; }
        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
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
                        Console.ReadLine();
                    }
                        return this;
                case PageControls.PageOption.AdjustCheckout:
                    if (applicationLogic.IsLoggedInAsCustomer)
                    {
                        CrudCreatorHelper.AdjustBasketItems(CurrentCustomer, applicationLogic);
                        applicationLogic.ComputerPartShopDB.SaveChanges();
                        applicationLogic.VerifyStoreItems();
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
                return;
            }
            //om inloggad, hämta nnuvarande kund
            CurrentCustomerId = appLol.CustomerId;
            CurrentCustomer = appLol.GetCustomerInfo(appLol.CustomerId);
            appLol.VerifyBasketItems(CurrentCustomerId);
            BasketProducts.Clear();
            BasketProducts = appLol.GetBasketProductsFromCustomerId((int)CurrentCustomerId);
        }

        public void RenderPage()
        {
            ConsoleHelper.ResetConsole();
            Graphics.PageBanners.DrawCheckoutPage();
            SetPageCommands();
            DrawAccountProfile();
            Console.WriteLine("Objects in basket: " + BasketProducts.Count);
            foreach (var product in BasketProducts) 
            {
                Console.WriteLine($"Id {product.Id} Name: {product.ComputerPart.Name} Quantity: {product.Quantity}");
            }
        }
        public void DrawAccountProfile()
        {
            List<string> tesList = new List<string>();
            if (CurrentCustomer != null)
            {
                tesList.AddRange(CurrentCustomer.FirstName, CurrentCustomer.SurName, CurrentCustomer.Email);
            }
            else
            {
                tesList.Add("Not Loggedin");
            }
            PageAccount.DrawAccountGraphic(tesList, "", ConsoleColor.DarkCyan);
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
