using ComputerStoreApplication.Account;
using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public class HomePage : IPage
    {
        List<ComputerPart> AllPartsForJoin = new List<ComputerPart>();
        List<ComputerPart>? SelectedProducts = new List<ComputerPart>();
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        public int? AdminId { get; set; }
        CustomerAccount? CurrentCustomer { get; set; }
        public int? CurrentCustomerId { get; set; }

        public void RenderPage()
        {
            Console.Clear();
            ConsoleHelper.ResetConsole();
            SetPageCommands();
            PageBanners.DrawShopBanner();
            
            DrawAccountProfile();
          
            Console.WriteLine("Products in focus;");
            Console.WriteLine($"Press {GetKeyFor(PageControls.PageOption.AddToBasket)} to add to basket");
            int left = 2;
            int top = 18;
            int spacing = 35;

            for (int i = 0; i < SelectedProducts.Count; i++)
            {
                var product = SelectedProducts[i];
                var rows = new List<string>
                {
                    $"Id: {product.Id}".PadRight(20),
                    product.Name.PadRight(20),
                    $"Price: {product.Price} €".PadRight(20),
                    (product.Sale ? "ON SALE!" : "(Not on sale)").PadRight(20)
                };
                var window = new MickesWindow.Window(
                   $"Offer {i + 1}",
                   left + (i * spacing),
                   top,
                   rows
               );

                window.Draw();

            }

        }
        public void DrawAccountProfile()
        {
            List<string> tesList = new List<string>();
            if (CurrentCustomer != null)
            {
                tesList.AddRange(CurrentCustomer.FirstName, CurrentCustomer.SurName, CurrentCustomer.Email, "Objects in basket: " +CurrentCustomer.ProductsInBasket.Count);
            }
            else
            {
                tesList.Add("Not Loggedin");
            }
            PageAccount.DrawAccountGraphic(tesList, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 15);
        }
        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this; //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Kolla page commands metoden
                case PageControls.PageOption.Home:
                    return this;
                case PageControls.PageOption.CustomerPage:
                    return new CustomerPage();
                case PageControls.PageOption.AdminPage:
                    return new AdminPage();
                case PageControls.PageOption.Browse:
                    return new BrowseProducts();
                case PageControls.PageOption.Checkout:
                    return new CheckoutPage();
                case PageControls.PageOption.AddToBasket:
                    //add to basket stuff
                    AddSomethingToBasket(applicationLogic);
                    return this;
                case PageControls.PageOption.CustomerLogin:
                    return new CustomerPage();
            }
            ;
            return this;

        }
        public void SetPageCommands()
        {
            //Specific commands per sida
            //Sparar kommandon till tangenter på sidor
            PageCommands = new Dictionary<ConsoleKey, PageControls.PageCommand>
            {
                { ConsoleKey.H, PageControls.HomeCommand },
                 {ConsoleKey.C, PageControls. CustomerHomePage},
                { ConsoleKey.B, PageControls.BrowseCommand },
                { ConsoleKey.A, PageControls.Admin },
                {ConsoleKey.L, PageControls.CustomerLogin },
                   {ConsoleKey.K, PageControls.Checkout },
                   {ConsoleKey.U, PageControls.AddToBasket }
            };
            //hitta beskrivningarna
            var pageOptions = PageCommands.Select(c => $"[{c.Key}] {c.Value.CommandDescription}").ToList();

            //Boom, rita dem
            if (pageOptions.Any())
            {
                Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkCyan);
            }
        }

        public void Load(ApplicationManager appLol)
        {
            //Random 3 objects every load
            if (SelectedProducts != null)
            {
                //SelectedProducts = appLol.GetFrontPageProducts().OrderBy(_ => Guid.NewGuid()).Take(5).ToList(); ;
            }
     
            //kolla om inloggad
            if (!appLol.IsLoggedInAsCustomer)
            {
                CurrentCustomerId = null;
                CurrentCustomer = null;
                //nullbara fält null, låt vara, gå vidare
                return;
            }
            //om inloggad, hämta nnuvarande kund
            CurrentCustomerId = appLol.CustomerId;
            CurrentCustomer = appLol.GetCustomerInfo(appLol.CustomerId);
         
        }
        public void AddSomethingToBasket(ApplicationManager appLo)
        {
            if (CurrentCustomer==null)
            {
                Console.WriteLine("You have to be logged in");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Please input the corresponding id of the product you wish to add to your basket");
            int choice = GeneralHelpers.StringToInt();
            if (choice == 0)
            {
                Console.WriteLine("Invalid, returning");
                Console.ReadLine();
                return;
            }
            var valid = SelectedProducts.FirstOrDefault(x => x.Id == choice);
            if (valid != null)
            {
                Console.WriteLine("Working...");
                appLo.AddProductToBasket(valid, CurrentCustomer);
                
            }
            else
            {
                Console.WriteLine("Not valid, returning...");
                Console.ReadLine();
                return;
            }
        }
        public ConsoleKey? GetKeyFor(PageControls.PageOption option)
        {
            return PageCommands
                .FirstOrDefault(p => p.Value.PageCommandOptionInteraction == option)
                .Key;
        }
    }
}
