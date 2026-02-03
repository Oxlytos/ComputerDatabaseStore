using ComputerStoreApplication.Account;
using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComputerStoreApplication.Helpers;
using System.Threading.Tasks;
using ComputerStoreApplication.Crud_Related;

namespace ComputerStoreApplication.Pages
{
    public class CustomerPage : IPage
    {
        public int? AdminId { get; set; }
        public int? CurrentCustomerId { get; set; }
        CustomerAccount? CurrentCustomer { get; set; }
        List<Order>? CustomerOrders { get; set; } = new List<Order>();

        public List<Country> Countries { get; set; } = new List<Country>();
        public List<City> Cities { get; set; } = new List<City>();
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        private List<Order> orders = new List<Order>();
        public void RenderPage()
        {
            Console.Clear();
            ConsoleHelper.ResetConsole();
            SetPageCommands();
            Graphics.PageBanners.DrawCustomerPage();

            DrawAccountProfile();

            PrintOrders();

        }
        public void PrintOrders()
        {
            Console.SetCursorPosition(0, 10); // start printing

            if (orders == null || !orders.Any())
            {
                Console.WriteLine("No orders found :(");
                return;
            }

            // Use DisplayHelpers to prepare display objects
            var displayOrders = orders
                .Select(o => DisplayHelpers.ToDisplay(o, Cities, Countries))
                .ToList();

            foreach (var order in orders) // orders is EF Order list
            {
                Console.WriteLine($"--- Order ID: {order.Id} ---");
                Console.WriteLine($"Creation Date: {order.CreationDate}");
                Console.WriteLine($"Subtotal: {order.Subtotal:N2} €");
                Console.WriteLine($"Total Cost: {order.TotalCost:N2} €");
                Console.WriteLine($"Shipping Cost: {order.ShippingCost:N2} €");
                Console.WriteLine($"Tax Costs: {order.TaxCosts:N2} €");
                Console.WriteLine($"Delivery Provider: {order.DeliveryProvider?.Name ?? "N/A (Error)"}");
                Console.WriteLine($"Payment Method: {order.PaymentMethod?.Name ?? "N/A (Error)"}");

                // Access shipping info directly
                if (order.ShippingInfo != null)
                {
                    Console.WriteLine($"--- Shipping Info ---");
                    Console.WriteLine($"Street: {order.ShippingInfo.StreetName}");
                    Console.WriteLine($"Postal Code: {order.ShippingInfo.PostalCode}");
                    Console.WriteLine($"City: {order.ShippingInfo.City?.Name ?? "N/A (Error)"}");
                    Console.WriteLine($"Country: {order.ShippingInfo.City?.Country?.Name ?? "N/A (Error)"}");
                }
                else
                {
                    Console.WriteLine("No shipping info available for this order.");
                }

                Console.WriteLine();
            }
            Console.WriteLine(); // spacing between orders
            }

        public void DrawAccountProfile()
        {
            List<string> accountInfo = new List<string>();
            accountInfo = PageAccount.ReturnCustomerProfileAccountString(CurrentCustomer);
            PageAccount.DrawAccountGraphic(accountInfo, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 10);
        }
        public async Task<IPage?> HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this; //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Kolla page commands metoden
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerLogin:
                    TryAndLogin(applicationLogic);
                    return this;
                case PageControls.PageOption.CreateAccount:
                    //Skapa konto
                    CreateAccount(applicationLogic);
                    return this;
                case PageControls.PageOption.CustomerPage:
                    return this;
                case PageControls.PageOption.CustomerShippingInfo:
                    //metod
                    HandleShippingInfo(applicationLogic);
                    return this;
                case PageControls.PageOption.Browse:
                    return new BrowseProducts();

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
                {ConsoleKey.L, PageControls.CustomerLogin },
                {ConsoleKey.P, PageControls.CustomerShippingInfo },
                {ConsoleKey.U, PageControls.CreateAccount }
            };
            //hitta beskrivningarna
            var pageOptions = PageCommands.Select(c => $"[{c.Key}] {c.Value.CommandDescription}").ToList();
            //Boom, rita dem
            if (pageOptions.Any())
            {
                Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkCyan);
            }
        }
        //varje reload
        public void Load(ApplicationManager appLol)
        {
            if (!appLol.IsLoggedInAsCustomer)
            {
                CurrentCustomerId = null;
                CurrentCustomer = null;
                orders.Clear();
                return;
            }

            CurrentCustomerId = appLol.CustomerId;
            CurrentCustomer = appLol.GetCustomerInfo(appLol.CustomerId);
            appLol.VerifyBasketItems(CurrentCustomerId);
            appLol.RefreshCurrentCustomerBasket(CurrentCustomer);
            appLol.VerifyStoreItems();
            appLol.VerifyBasketItems(CurrentCustomerId);

            Countries = appLol.ComputerPartShopDB.Countries.ToList();
            Cities = appLol.ComputerPartShopDB.Cities.Include(c => c.Country).ToList();
            Console.WriteLine($"CustomerId: {appLol.CustomerId}");
            var anyOrders = appLol.ComputerPartShopDB.Orders.ToList();
            Console.WriteLine($"Total orders in DB: {anyOrders.Count}");
            // DEBUG: check if CustomerId is valid
            Console.WriteLine($"Loading orders for Customer ID: {appLol.CustomerId}");

            orders = appLol.ComputerPartShopDB.Orders
              .AsNoTracking()
              .Include(o => o.OrderItems)
                  .ThenInclude(oi => oi.ComputerPart)
              .Include(o => o.ShippingInfo)
                  .ThenInclude(s => s.City)
                      .ThenInclude(c => c.Country)
              .Include(o => o.DeliveryProvider)
              .Include(o => o.PaymentMethod)
              .Where(o => o.CustomerId == appLol.CustomerId)
              .ToList();

      
        }


        public void TryAndLogin(ApplicationManager app)
        {
            if (app.IsLoggedInAsAdmin)
            {
                Console.WriteLine("Can't login as customer while in admin login");
                Console.ReadLine();
                return;
            }
            if (app.IsLoggedInAsCustomer)
            {
                app.LogoutAsCustomer();
                return;
            }
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("Email?");
            string email = Console.ReadLine();
            Console.WriteLine("Password?");
            string password = Console.ReadLine();
            app.LoginAsCustomer(email, password);
        }
      
        public void CreateAccount(ApplicationManager app)
        {
            Console.WriteLine("Email?");
            string email = Console.ReadLine();
            app.CreateAccount(email);
        }
        public void HandleShippingInfo(ApplicationManager app)
        {
            if (CurrentCustomer != null)
            {
                Console.Clear();
                CrudHandler.CRUDShippingInfo(app,CurrentCustomer);
               // app.HandleCustomerShippingInfo(CurrentCustomer.Id);
            }
        }
    }
}
