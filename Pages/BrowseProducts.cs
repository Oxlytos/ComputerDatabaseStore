using ComputerStoreApplication.Account;
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
    public class BrowseProducts : IPage
    {
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        public int? AdminId { get; set; }
        public AdminAccount? Admin { get; set; }
        public int? CurrentCustomerId { get; set; }
        CustomerAccount? CurrentCustomer { get; set; }
        List<ComputerPart> Products { get; set; } = new List<ComputerPart>();

        public void Load(ApplicationManager appLol)
        {
            
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
        public void RenderPage(ApplicationManager applicationLogic)
        {
            Console.Clear();
            SetPageCommands();
            Graphics.PageBanners.DrawBrowsePageBanner();
            Console.SetCursorPosition(0, 10);
            DrawAccountProfile(applicationLogic);
            Console.WriteLine("Our wide cataloge of things, down below!");
            if (Products != null || Products.Count > 0)
            {
                foreach (var product in Products) 
                {
                    if (product.Stock > 0)
                    {
                        string onSale = product.Sale ? "Yes" : "No";
                        Console.WriteLine($"Id: [{product.Id}] Name: [{product.Name}]\t Price: [{product.Price}]€\t On Sale?: [{onSale}] Category: [{product.ComponentCategory.Name}]\t Brand:[{product.BrandManufacturer.Name}]");
                    }
                }
                Console.WriteLine("");
            }
        }
        public void DrawAccountProfile(ApplicationManager applicationLogic)
        {
            List<string> accountInfo = new List<string>();
            accountInfo = PageAccount.ReturnCustomerProfileAccountString(applicationLogic);
            PageAccount.DrawAccountGraphic(accountInfo, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 15);

        }
        public async Task<IPage?> HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this; //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Bokstaven N är skapa ny produkt, vi laddar om samma sida, fast kallar en metod innan
                case PageControls.PageOption.Checkout:
                    return new CheckoutPage() ;
                case PageControls.PageOption.ViewObject:
                    CheckoutObject(applicationLogic);
                    return new BrowseProducts();
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
                {ConsoleKey.F, PageControls.Search},
                {ConsoleKey.K, PageControls.Checkout },
                {ConsoleKey.V, PageControls.ViewObject }
            };
            //hitta beskrivningarna
            var pageOptions = PageCommands.Select(c => $"[{c.Key}] {c.Value.CommandDescription}").ToList();

            //Boom, rita dem
            if (pageOptions.Any())
            {
                Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkCyan);
            }
        }
        public void CheckoutObject(ApplicationManager app)
        {
            Console.WriteLine("What object do you wanna view, and maybe add to your basket? Input their corresponding Id");
            var products = app.GetDBProductsFromList(Products);
            var choosenObject = app.ChooseProductFromList(products);
            if (choosenObject == null)
            {
                return;
            }
            Console.WriteLine($"Id: {choosenObject.Id} Name; {choosenObject.Name}");

            var category = app.GetCategory(choosenObject.Id) ?? throw new Exception("Could not find category");
            var brand = app.GetBrand(choosenObject.Id) ?? throw new Exception("Could not find brand");
            choosenObject.Read(brand, category);
            Console.SetCursorPosition(5, 40);
            Console.WriteLine("Add to basket?");
            bool yes = GeneralHelpers.ChangeYesOrNo(false);
            if (!yes)
            {
                app.InformOfQuittingOperation();
                return;
            }
            if (yes && CurrentCustomer != null)
            {
                app.AddProductToBasket(choosenObject, CurrentCustomerId.Value);
            }
            else if (CurrentCustomer == null)
            {
                Console.WriteLine("You need to be logged in to add to basket");
                app.InformOfQuittingOperation();
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
