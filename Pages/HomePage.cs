using ComputerStoreApplication.Graphics;
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
            foreach (var product in SelectedProducts)
            {
                Console.WriteLine($"Name: {product.Name} Price: {product.Price} On Sale?:{product.Sale} (Hidden) ProductId: {product.StoreProductId}");
                var actualProduct = AllPartsForJoin.FirstOrDefault(X=>X.Id==product.ComputerPartId);
                Console.WriteLine($"\t- {actualProduct.Name} ");
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
            AllPartsForJoin = appLol.ComputerPartShopDB.AllParts.ToList();
        }
    }
}
