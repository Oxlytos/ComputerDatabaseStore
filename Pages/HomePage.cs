using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
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
        List<StoreProduct> SelectedProducts = new List<StoreProduct>();
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        Customer? CurrentCustomer { get; set; }
        public int? CurrentCustomerId { get; set; }

        public void RenderPage()
        {
            Console.Clear();
            SetPageCommands();
            PageBanners.DrawShopBanner();
            Console.SetCursorPosition(0, 10);
            if (CurrentCustomer != null)
            {
                Console.WriteLine($"Logged in as {CurrentCustomer.FirstName} {CurrentCustomer.SurName}");
            }
            else
            {
                Console.WriteLine("Not logged in");
            }
            Console.WriteLine("Products in focus;");
            Console.WriteLine("If you wanna add something to your basket, press 'K'!");
            Console.WriteLine("Selected products count: " + SelectedProducts.Count);
            foreach (var product in SelectedProducts)
            {
                Console.WriteLine($" Id: {product.Id}\tName: {product.Name} Description: {product.Description} Price: {product.Price} On Sale?:{product.Sale} (Hidden)");
            }
            
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
            SelectedProducts = appLol.GetFrontPageProducts();
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
            int choice = GeneralHelpers.StringToInt(Console.ReadLine());
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
    }
}
