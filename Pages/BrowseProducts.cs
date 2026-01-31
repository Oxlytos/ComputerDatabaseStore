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

        ComputerPart Product { get; set; }
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
            DrawAccountProfile();
            Console.WriteLine("What's available, down below!");
            Console.WriteLine("Press 'A' to add to basket!");
            if (Products != null || Products.Count > 0)
            {
                foreach (var product in Products) 
                {
                    if (product.Stock > 0)
                    {
                        Console.WriteLine(
                        $"Id: {product.Id}\n" +
                        $"Name: {product.Name}\n"
                        );

                    }

                }
                
            }

        }
        public void DrawAccountProfile()
        {
            List<string> tesList = new List<string>();
            if (CurrentCustomer != null)
            {
                tesList.AddRange(CurrentCustomer.FirstName, CurrentCustomer.SurName, CurrentCustomer.Email, "Objects in basket: " + CurrentCustomer.ProductsInBasket.Count);
            }
            else
            {
                tesList.Add("Not Loggedin");
            }
            PageAccount.DrawAccountGraphic(tesList, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 15);

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
                case PageControls.PageOption.ViewObject:
                    CheckoutObject(applicationLogic);
                    return new CheckoutPage();
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerPage:
                    return new CustomerPage();
                case PageControls.PageOption.AddToBasket:
                    return this;
                    //Produktvy och lägg kanske till i basket
                    return this;
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
                {ConsoleKey.A, PageControls.AddToBasket },
                {ConsoleKey.B, PageControls.BrowseCommand},
                {ConsoleKey.L, PageControls.CustomerLogin },
                {ConsoleKey.Q, PageControls.CustomerLogout },
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
            int choice = GeneralHelpers.StringToInt();
            var validObject = Products.FirstOrDefault(s=>s.Id== choice);
            if (validObject != null) 
            {
               bool yes =  StoreHelper.ViewProduct(validObject, app);
                if (yes && CurrentCustomer!=null) 
                {
                    app.AddProductToBasket(validObject,CurrentCustomer);
                }
                if (CurrentCustomer == null)
                {
                    Console.WriteLine("You need to be logged in to add to basket");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.WriteLine("Quitting operation");
                    Console.ReadLine();
                    return;
                }
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
