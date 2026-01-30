using ComputerStoreApplication.Account;
using ComputerStoreApplication.Crud_Related;
using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Helpers.DTO;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
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

        private List<ComponentOrderCount> mostPopularOrders = new List<ComponentOrderCount>();
        private List<CategoryOrderCount> mostpopularCaetgory = new List<CategoryOrderCount>();
        private List<MostExpensiveOrders> mostExpensiveOrders = new List<MostExpensiveOrders>();

        public void SetPageCommands()
        {
            //Specific commands per sida
            //Sparar kommandon till tangenter på sidor
            PageCommands = new Dictionary<ConsoleKey, PageControls.PageCommand>
            {
                { ConsoleKey.H, PageControls.HomeCommand },
                { ConsoleKey.A, PageControls.Admin },
                {ConsoleKey.K, PageControls.AdminLogin },
                {ConsoleKey.I, PageControls.AdminLogout },
                {ConsoleKey.N, PageControls.AdminCreateProduct },
                {ConsoleKey.M, PageControls.AdminCreateCategory },
                {ConsoleKey.P, PageControls.AdminCreateStoreProduct },
                { ConsoleKey.C, PageControls.AdminEditCustomerProfile},
                {ConsoleKey.B, PageControls.AddNewBrand },
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
                ConsoleHelper.WriteCentered("Popular orders");
                ConsoleHelper.WriteCenteredEmptyLine();
                foreach(var prod in mostPopularOrders)
                {
                    ConsoleHelper.WriteCentered($"{prod.ComponentName} appeared in {prod.OrdersCount} orders");
                }
                ConsoleHelper.WriteCenteredEmptyLine();
                ConsoleHelper.WriteCentered("Most popular categories");
                ConsoleHelper.WriteCenteredEmptyLine();
                foreach (var cat in mostpopularCaetgory)
                {
                    ConsoleHelper.WriteCentered($"{cat.Category} has {cat.OrdersCount} active orders");
                }
                ConsoleHelper.WriteCentered("Most expensive orders");
                ConsoleHelper.WriteCenteredEmptyLine();
                foreach (var expen in mostExpensiveOrders)
                {
                    ConsoleHelper.WriteCentered($"Product {expen.ProductName} was orded by {expen.CustomerName}, which went for {expen.TotalCost}€");
                }
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
            else
            {
                tesList.AddRange("Not Loggedin", "Admin Operations not available");
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
                case PageControls.PageOption.AdminAddNewBrand:
                    if (!applicationLogic.IsLoggedInAsAdmin) { return this; }
                    Crud_Related.CrudHandler.AddNewBrand(applicationLogic);
                    return this; //This blir denna sida
                case PageControls.PageOption.AdminCreateComponent:
                    if (!applicationLogic.IsLoggedInAsAdmin) { return this; }
                    Crud_Related.CrudHandler.ComponentInput(applicationLogic);
                    return this; //This blir denna sida
                case PageControls.PageOption.AdminCreateCategory:
                    if (!applicationLogic.IsLoggedInAsAdmin) { return this; }
                    Crud_Related.CrudHandler.CategoryInput(applicationLogic);
                    return this;
                case PageControls.PageOption.AdminCreateStoreProduct:
                    if (!applicationLogic.IsLoggedInAsAdmin) { return this; }
                    Crud_Related.CrudHandler.StoreProductInput(applicationLogic);
                    return this;
                case PageControls.PageOption.AdminEditCustomerProfile:
                    if (!applicationLogic.IsLoggedInAsAdmin) { return this; }
                    applicationLogic.EditCustomerProfile();
                    return this;
                case PageControls.PageOption.AdminLogin:
                    LoginAdmin(applicationLogic);
                    return this;
                case PageControls.PageOption.AdminLogout:
                    if (!applicationLogic.IsLoggedInAsAdmin) { return this; }
                    LogoutAdmin(applicationLogic);
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
            GetStats(appLol);
           
            //om inloggad, hämta nnuvarande admin
            AdminId = appLol.AdminId;
            AdminAccount = appLol.GetAdminInfo(appLol.AdminId);
        }
        public async Task GetStats(ApplicationManager app)
        {
            mostPopularOrders = await app.Dapper.GetMostPopularOrders();
            mostpopularCaetgory = await app.Dapper.GetMostPopularCategories();
            mostExpensiveOrders = await app.Dapper.GetMostExpensiveOrders();
        }
    }
}
