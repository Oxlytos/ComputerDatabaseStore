using ComputerStoreApplication.Account;
using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
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

        List<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();
        List<ComputerPart> ComputerParts { get; set; }

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
                    if (BasketProducts.Any())
                    {
                        applicationLogic.HandleCustomerPurchase(CurrentCustomer.Id);
                    }
                    else
                    {
                        Console.WriteLine("Basket empty, returning");
                        Console.ReadLine();
                    }
                        return new CustomerPage();
                case PageControls.PageOption.AdjustCheckout:
                    if (applicationLogic.IsLoggedInAsCustomer)
                    {
                        //Mismatch between page loading and updating logic
                        //Calc needs to be done here, or else numbers wont update
                        BasketProduct bask;
                        if (BasketProducts.Count == 1)
                            bask = BasketProducts.First();
                        else
                            bask = StoreHelper.ChooseWhichBasketItem(BasketProducts);

                        if (bask != null)
                        {
                            StoreHelper.AdjustQuantityOfBasketItems(bask);
                            if (bask.Quantity == 0||bask.Quantity<0)
                            {
                                Console.WriteLine("Removing product, it has a quantity of 0");
                                CurrentCustomer.ProductsInBasket.Remove(bask);
                                applicationLogic.ComputerPartShopDB.Remove(bask);
                                Console.ReadLine() ;
                            }
                            applicationLogic.ComputerPartShopDB.SaveChanges();
                        }
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
            var basketProducts = appLol.ComputerPartShopDB.BasketProducts
            .Include(bp => bp.ComputerPart) // Product navigation property
            .Where(bp => bp.CustomerId == CurrentCustomerId)
            .ToList();
            ComputerParts = appLol.GetStoreProducts();

            BasketProducts = basketProducts;
        }

        public void RenderPage()
        {
            Graphics.PageBanners.DrawCheckoutPage();
            ConsoleHelper.ResetConsole();
            SetPageCommands();
            DrawAccountProfile();
            if (BasketProducts.Count > 0) 
            {
                foreach (var basketItem in BasketProducts)
                {
                    if (basketItem.ComputerPart != null)
                    {
                        Console.WriteLine($"Product: {basketItem.ComputerPart.Name} €: {basketItem.ComputerPart.Price} Quantity [{basketItem.Quantity}] Id: {basketItem.Id}");
                    }
                    else
                    {
                        Console.WriteLine($"Product Id {basketItem.ComputerPartId} not found. Quantity [{basketItem.Quantity}] Id: {basketItem.Id}");
                    }
                    
                }
            }
            else
            {
                Console.WriteLine("Empty basket");
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
