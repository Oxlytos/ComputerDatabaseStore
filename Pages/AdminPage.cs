using ComputerStoreApplication.Account;
using ComputerStoreApplication.Crud_Related;
using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Helpers.DTO;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
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
        //private List<StoreProduct> products = new List<StoreProduct>();
        //private List<ComponentOrderCount> mostPopularOrders = new List<ComponentOrderCount>();
        //private List<CategoryOrderCount> mostpopularCaetgory = new List<CategoryOrderCount>();
        //private List<MostExpensiveOrders> mostExpensiveOrders = new List<MostExpensiveOrders>();
        //private List<MostPopularProductByCountry> mostPopularProductByCountries = new List<MostPopularProductByCountry>();
        //private List<CountrySpending> countrySpendings = new List<CountrySpending>();
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
            };
            //hitta beskrivningarna
            var pageOptions = PageCommands.Select(c => $"[{c.Key}] {c.Value.CommandDescription}").ToList();

            //Boom, rita dem
            if (pageOptions.Any())
            {
                Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkCyan);
            }
        }
        public void RenderPage()
        {
            Console.Clear();
            SetPageCommands();

            Graphics.PageBanners.DrawAdmingBanner();
            Console.SetCursorPosition(0, 15);
            if (AdminAccount != null)
            {
              
            }
            else
            {
            }
            DrawAccountProfile();
        }
        public void DrawAccountProfile()
        {

            List<string> tesList = new List<string>();
            if (AdminAccount != null)
            {
                tesList.AddRange(AdminAccount.UserName, AdminAccount.FirstName, AdminAccount.SurName, AdminAccount.PhoneNumber);
            }
            if (CustomerAccount != null)
            {
                tesList.AddRange(CustomerAccount.FirstName, CustomerAccount.SurName, CustomerAccount.PhoneNumber, "Admin Operations not available ");
            }
            else
            {
                tesList.AddRange("Not lgged in as Admin", "Admin Operations not available");
            }
            PageAccount.DrawAccountGraphic(tesList, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 10);

        }
        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this; //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Bokstaven N är skapa ny produkt, vi laddar om samma sida, fast kallar en metod innan
                case PageControls.PageOption.AdminCreate:
                    if (!applicationLogic.IsLoggedInAsAdmin) { return this; }
                    Console.Clear();
                    CrudHandler.ChooseCategory(applicationLogic);
                    applicationLogic.SaveChangesOnComponent();
                    return this; //This blir denna sida
                case PageControls.PageOption.AdminLogin:
                    Console.SetCursorPosition(0, 15);
                    if (!applicationLogic.IsLoggedInAsAdmin)
                    {
                        LoginAdmin(applicationLogic);
                    }
                    else if (applicationLogic.IsLoggedInAsAdmin)
                    {
                        LogoutAdmin(applicationLogic);
                    }
                        return this;
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerPage:
                    return new CustomerPage();
            }
            ;
            return this;

        }
        public void LoginAdmin(ApplicationManager app)
        {
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
        public void AdminAction(ApplicationManager logic)
        {
            Console.WriteLine("Choose what action to peform on a category");
            Console.WriteLine("Register a product to storage");
            Console.WriteLine("Create a new store product");
            Console.WriteLine("");
            Console.ReadLine();
            Console.WriteLine("What CRUD action?");
        }
        public async Task GetStats(ApplicationManager app)
        {
            //mostPopularOrders = await app.Dapper.GetMostPopularOrders();
            //mostpopularCaetgory = await app.Dapper.GetMostPopularCategories();
            //mostExpensiveOrders = await app.Dapper.GetMostExpensiveOrders();
            //mostPopularProductByCountries = await app.Dapper.GetMostPopularProductByCountry();
            //countrySpendings = await app.Dapper.GetCountryWithTheMostSpending();
            //totalRevenue = await app.Dapper.GetTotalRevenue();
        }
    }
}
