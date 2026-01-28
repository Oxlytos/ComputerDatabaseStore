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
using static ComputerStoreApplication.Pages.PageControls;

namespace ComputerStoreApplication.Pages
{
    internal class SearchedResults : IPage
    {

        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        Customer? CurrentCustomer { get; set; }

        public int? CurrentCustomerId {get; set; }

        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this; //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Kolla page commands metoden
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerPage:
                    return new CustomerPage();
                case PageControls.PageOption.AdminPage:
                    return new AdminPage();
                case PageControls.PageOption.Search:
                    SearchResults(applicationLogic);
                    return this;
            }
            ;
            return this;

        }

        //Basic
        public void SearchResults(ApplicationManager appLol)
        {
            Console.WriteLine("Input search query, please");
            string input = Console.ReadLine();

            //sök
            var allProducts = appLol.GetStoreProducts();
            List<StoreProduct> parts = new List<StoreProduct>();
            foreach (StoreProduct part in allProducts)
            {
                if (part.Name.ToLower().Contains(input.ToLower()))
                {
                    parts.Add(part);
                }
            }
            //printa alla resultat
            Console.WriteLine($"Found this many similar objects based on query results: {parts.Count}");
            if (parts.Count > 0)
            {
                foreach (StoreProduct part in parts)
                {
                    Console.WriteLine(part.Id + " " + part.Name);
                }
            }
            Console.WriteLine("Do any of these objects catch your eye? Input their corresponding Id number to add to your personal basket!");
            int choice = GeneralHelpers.StringToInt(Console.ReadLine());

            if (CurrentCustomer != null)
            {
                var doesItExist = parts.FirstOrDefault(x => x.Id == choice);
                if (doesItExist != null)
                {
                    Console.WriteLine("Exists!");
                    Console.WriteLine($"You wanna add {doesItExist.Name} to your basket?");
                    bool confirmation = GeneralHelpers.YesOrNoReturnBoolean(Console.ReadLine());
                    if (confirmation)
                    {
                        appLol.AddProductToBasket(doesItExist, CurrentCustomer);
                    }
                    else
                    {
                        Console.WriteLine("Quitting operation...");
                    }
                }
                else
                {
                    Console.WriteLine("Dosen't exist");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You can only add to basket if your logged in");
                Console.ReadLine();
                return;
            }
        }
        public void Load(ApplicationManager appLol)
        {
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

        public void RenderPage()
        {
            Console.Clear();
            SetPageCommands();
            Graphics.PageBanners.DrawSearchedResults ();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Browse product page");
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
                { ConsoleKey.F, PageControls.Search },
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
