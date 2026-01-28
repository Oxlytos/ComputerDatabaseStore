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
    public class BrowseProducts : IPage
    {
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;

        public int? CurrentCustomerId { get; set; }
        Customer? CurrentCustomer { get; set; }

        List<StoreProduct> Products { get; set; } = new List<StoreProduct>();
        public void Load(ApplicationManager appLol)
        {
            //kolla om inloggad
            Products = appLol.GetStoreProducts();
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
        public void RenderPage()
        {
            Console.Clear();
            SetPageCommands();
            Graphics.PageBanners.DrawBrowsePageBanner();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Browse product page");
            Console.WriteLine("TODO ADD 'aDD TO BASKET' ");
            if (CurrentCustomer != null)
            {
                Console.WriteLine($"Logged in as {CurrentCustomer.FirstName} {CurrentCustomer.SurName}");
            }
            else
            {
                Console.WriteLine("Not logged in");
            }
            Console.WriteLine("What's available, down below!");
            if (Products != null || Products.Count > 0)
            {
                foreach (var product in Products) 
                {
                    Console.WriteLine($"\tId: {product.Id} Name: {product.Name} Description: {product.Description} Price: {product.Price} Sale: {product.Sale}");
                }
                
            }

        }
        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            //har vi inte deras input
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this; //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Bokstaven N är skapa ny produkt, vi laddar om samma sida, fast kallar en metod innan
                case PageControls.PageOption.Checkout:
                    return new CheckoutPage() ;
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerPage:
                    return new CustomerPage();
                case PageControls.PageOption.Search:
                    return new SearchedResults();
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
                {ConsoleKey.B, PageControls.BrowseCommand},
                {ConsoleKey.L, PageControls.CustomerLogin },
                {ConsoleKey.Q, PageControls.CustomerLogout },
                {ConsoleKey.F, PageControls.Search},
                {ConsoleKey.K, PageControls.Checkout }
            };
            //hitta beskrivningarna
            var pageOptions = PageCommands.Select(c => $"[{c.Key}] {c.Value.CommandDescription}").ToList();

            //Boom, rita dem
            if (pageOptions.Any())
            {
                Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkCyan);
            }
        }
        public void TryAndLogin(ApplicationManager app)
        {
            Console.WriteLine("Email?");
            string email = Console.ReadLine();
            Console.WriteLine("Password?");
            string password = Console.ReadLine();
            app.LoginAsCustomer(email, password);
        }


    }
}
