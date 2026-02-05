using ComputerStoreApplication.Account;
using ComputerStoreApplication.Crud_Related;
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
    public class AdminPage : IPage
    {
     
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;

        public int? AdminId {get; set;}

        public AdminAccount? AdminAccount {get; set;}
     
        public int? CurrentCustomerId {get; set;}
        public CustomerAccount? CustomerAccount { get; set; }
        private decimal totalRevenue;
        public void SetPageCommands()
        {
            //Specific commands per page
            PageCommands = new Dictionary<ConsoleKey, PageControls.PageCommand>
            {
                { ConsoleKey.H, PageControls.HomeCommand }, 
                { ConsoleKey.A, PageControls.Admin },
                {ConsoleKey.L, PageControls.AdminLogin },
                {ConsoleKey.C, PageControls.AdminCreate },
                {ConsoleKey.V, PageControls.AdminViewStats },
            };
            //hitta beskrivningarna
            var pageOptions = PageCommands.Select(c => $"[{c.Key}] {c.Value.CommandDescription}").ToList();

            //Boom, rita dem
            if (pageOptions.Any())
            {
                Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkCyan);
            }
        }
        public void RenderPage(ApplicationManager applicationLogic)
        {
            Console.Clear();
            SetPageCommands();

            Graphics.PageBanners.DrawAdmingBanner();
            Console.SetCursorPosition(0, 15);
          
            DrawAccountProfile(applicationLogic);
        }
        public void DrawAccountProfile(ApplicationManager applicationLogic)
        {
            List<string> accountInfo = new List<string>();
            accountInfo = PageAccount.ReturnAdminProfileAccountString(applicationLogic);
            PageAccount.DrawAccountGraphic(accountInfo, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 10);
        }
        public async Task<IPage?> HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this;
            //This blir denna sida //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Bokstaven N är skapa ny produkt, vi laddar om samma sida, fast kallar en metod innan
                case PageControls.PageOption.AdminCreate:
                    if (!applicationLogic.IsLoggedInAsAdmin) 
                    {
                        return this; 
                    }
                    Console.Clear();
                    CrudHandler.ChooseCategory(applicationLogic);
                    return this;
                case PageControls.PageOption.AdminLogin:
                    Console.SetCursorPosition(0, 15);
                    //try and login
                    if (applicationLogic.IsLoggedInAsCustomer)
                    {

                    }
                    if (!applicationLogic.IsLoggedInAsAdmin)
                    {
                        LoginAdmin(applicationLogic);
                    }
                    //if logged in, try and logout
                    else if (applicationLogic.IsLoggedInAsAdmin)
                    {
                        LogoutAdmin(applicationLogic);
                    }
                    return this;
                case PageControls.PageOption.AdminViewStats:
                    //show stats if logged in admin
                    if (!applicationLogic.IsLoggedInAsAdmin)
                    {
                        Console.WriteLine("You need to be logged in as an admin to view these statistics");
                        Console.ReadKey();
                    }
                    if (applicationLogic.IsLoggedInAsAdmin)
                    {
                          await ViewStats(applicationLogic);
                    }
                    return this;
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerPage:
                    return new CustomerPage();
            }
            return this;
        }

        public async Task ViewStats(ApplicationManager app)
        {
            ///try and get queries via dapper for stats
            try
            {
                decimal totalRevenue = await app.Dapper.GetTotalRevenue();
                var mostProfitableBrand = await app.Dapper.GetMostProfitableBrand();
                var deliveryStats = await app.Dapper.GetOrdersPerDeliveryService();
                var mostExpensiveOrderLastDay = await app.Dapper.GetMostExpensiveOrderLast24Hours();
                //var top3mostmoneyspentcountries = await app.Dapper.GetCountryWithTheMostSpending();
                var countrySpending = await app.Dapper.GetTotalCountrySpending();
                var biggestSpender = await app.Dapper.GetHighestSpender();
                var leastAmountSpent = await app.Dapper.GetLowestSpender();
                var mostCommonPayMethod = await app.Dapper.GetMostCommonPayMethod();
                var mostCommonCategoryPerCity = await app.Dapper.GetMostCommanCategoryPerCity();

                // Display stats neatly
                Console.WriteLine("\n ---- Admin Info --------\n");
                Console.WriteLine($"Total Revenue (€): {totalRevenue:N2}");
                Console.WriteLine($"Most Profitable (€) Brand: {mostProfitableBrand?.BrandName ?? "Not available"} ({mostProfitableBrand?.TotalRevenue})\n");

                if (mostExpensiveOrderLastDay.HasValue)
                {
                    Console.WriteLine($"Most expensive order last 24 hours is {mostExpensiveOrderLastDay.Value.TotalValue} € by account {mostExpensiveOrderLastDay.Value.Email}");
                }
                Console.WriteLine("Orders Per Delivery Service:");
                foreach (var d in deliveryStats)
                {
                    Console.WriteLine($"{d.DeliveryProviderName.PadRight(25)} : {d.NumberOfOrders} orders, handling wares with a total value of {d.TotalValue}");
                }

                Console.WriteLine("\nCity Spending (€):");
                Console.WriteLine("We got orders in these places; ");
                foreach(var d in countrySpending)
                {
                    Console.WriteLine($"{d.City} (in {d.Country}) at {d.TotalValue}€");
                }
                Console.WriteLine("\nTotal spent per country");
                //foreach (var c in top3mostmoneyspentcountries)
                //{
                //    Console.WriteLine($"{c.Value.CountryName} spent {c.Value.TotalSpent} in total!");
                //}
                Console.WriteLine("\nMost common category per city");
                foreach(var c in mostCommonCategoryPerCity)
                {
                    Console.WriteLine($"{c.CityName} has category {c.PartCategory} as their most common (based on units), with {c.TotalProducts} sold/delivered there");
                }
                if(biggestSpender != null)
                {
                    Console.WriteLine("\nHighest spender (€) is; ");
                    Console.WriteLine($"{biggestSpender.Value.FirstName} {biggestSpender.Value.SurName} with an expendeture of:  {biggestSpender.Value.TotalSpent} (€)");
                }
                if (leastAmountSpent != null)
                {
                    Console.WriteLine("\nAnd the lowest amount spent (€) by a person is;");
                    Console.WriteLine($"{leastAmountSpent.Value.FirstName} {leastAmountSpent.Value.SurName} with an expendeture of: {leastAmountSpent.Value.TotalSpent} (€)");
                }
                if (mostCommonPayMethod != null) 
                {
                    Console.WriteLine("\nMost common pay method is;");
                    foreach(var p in mostCommonPayMethod)
                    {
                        Console.WriteLine($"{p.PayName} with {p.Count}");
                    }
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching stats: {ex.Message}");
            }
        }
        public void LoginAdmin(ApplicationManager app)
        {
            if (app.IsLoggedInAsCustomer)
            {
                    Console.WriteLine("Can't login as a admin while logged in as a customer");
                    Console.ReadLine();
                return;
            }
            Console.WriteLine("Admin username?");
            string username = Console.ReadLine();
            Console.WriteLine("Admin password?");
            string password = Console.ReadLine();
            if (username != null && password != null) 
            {
                app.LoginAsAdmin(username, password);

                Load(app);
            }
            else
            {
                _ = MongoConnection.AdminLoginAttempt(username, 0, false);
                return;
            }
            
        }
        public void LogoutAdmin(ApplicationManager app)
        {
            Console.WriteLine("Do you wish to logout?");
            bool result = GeneralHelpers.YesOrNoReturnBoolean();
            if (result)
            {
                app.LogoutAsAdmin();
            }
        }
        public void Load(ApplicationManager appLol)
        {
            if (!appLol.IsLoggedInAsAdmin)
            {
                AdminId = null;
                AdminAccount = null;
                //nullbara fält null, låt vara, gå vidare
                return;
            }
           
            AdminId = appLol.AdminId;
            AdminAccount = appLol.GetAdminInfo(appLol.AdminId);
        }
     
    }
}
