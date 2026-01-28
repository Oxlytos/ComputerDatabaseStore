using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public class CustomerPage : IPage
    {
        public int? CurrentCustomerId { get; set; }
        Customer? CurrentCustomer { get; set; }
        List<Order>? CustomerOrders { get; set; }
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        public void RenderPage()
        {
            Console.Clear();
            SetPageCommands();
            Graphics.PageBanners.DrawCustomerPage();
            Console.SetCursorPosition(0, 10);
            if (CurrentCustomer != null)
            {
                Console.WriteLine($"Logged in as {CurrentCustomer.FirstName} {CurrentCustomer.SurName}");
                if (CustomerOrders != null && CustomerOrders.Any()) 
                {
                    foreach (var order in CustomerOrders) 
                    {
                        Console.WriteLine($"Order Id: {order.Id} \n Costs (Total): {order.TotalCost} Tax: {order.TaxCosts} Pre-Tax:{order.Subtotal}");
                        foreach (var prod in order.OrderItems) 
                        {
                            Console.WriteLine($"\tProduct: {prod.Product.Name} Amount: {prod.Quantity} Pricer per unit: {prod.Price} ");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Not logged in");
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
                    return new HomePage();
                case PageControls.PageOption.CustomerLogin:
                    TryAndLogin(applicationLogic);
                    return this;
                case PageControls.PageOption.CustomerLogout:
                    applicationLogic.LogoutAsCustomer();
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
                case PageControls.PageOption.Checkout:
                    return new CheckoutPage();
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
                {ConsoleKey.Q, PageControls.CustomerLogout },
                {ConsoleKey.K, PageControls.Checkout },
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
            CustomerOrders = appLol.GetCustomerOrders(appLol.CustomerId);
        }

        public void TryAndLogin(ApplicationManager app)
        {
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
                app.HandleCustomerShippingInfo(CurrentCustomer.Id);
            }
        }
    }
}
