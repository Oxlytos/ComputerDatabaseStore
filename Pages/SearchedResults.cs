using ComputerStoreApplication.Account;
using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static ComputerStoreApplication.Pages.PageControls;

namespace ComputerStoreApplication.Pages
{
    internal class SearchedResults : IPage
    {
        public int? AdminId { get; set; }
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        CustomerAccount? CurrentCustomer { get; set; }

        public int? CurrentCustomerId {get; set; }

        public async Task<IPage?> HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
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
                    await SearchResults(applicationLogic);
                    return this;
            }
            ;
            return this;

        }

        //Basic search
        public async Task SearchResults(ApplicationManager appLol)
        {
           List<ComputerPart>? parts = StoreHelper.SearchResults(appLol.GetStoreProducts());
            //printa alla resultat
            if (!parts.Any())
            {
                Console.WriteLine("No objects based on search term....");
                appLol.InformOfQuittingOperation();
            }
            Console.WriteLine($"Found this many similar objects based on query results: {parts.Count}");
            if (parts.Count > 0)
            {
                foreach (ComputerPart part in parts)
                {
                    string onSale = part.Sale ? "Yes":"No";
                    Console.WriteLine($"Id: {part.Id.ToString().PadRight(5)}| Name: {part.Name} | Category: {part.ComponentCategory.Name} | Price: {part.Price} | On Sale? {onSale}");
                }
            }
            Console.WriteLine("Do any of these objects catch your eye? Input their corresponding Id number to add to your personal basket, or 0 to return");
            
            var objectToAdd = StoreHelper.DecideToAddToBasket(CurrentCustomerId, parts);
            if(objectToAdd == null)
            {
                appLol.InformOfQuittingOperation();
                return;
            }
            appLol.AddProductToBasket(objectToAdd, CurrentCustomerId.Value);
        }
        public void DrawAccountProfile(ApplicationManager applicationManager)
        {
            List<string> accountInfo = new List<string>();
            accountInfo = PageAccount.ReturnCustomerProfileAccountString(applicationManager);
            PageAccount.DrawAccountGraphic(accountInfo, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 10);
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

        public void RenderPage(ApplicationManager applicationManager)
        {
            Console.Clear();
            ConsoleHelper.ResetConsole();
            SetPageCommands();
            Graphics.PageBanners.DrawSearchedResults();
            DrawAccountProfile(applicationManager);
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
