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
    public class HomePage : IPage
    {
        List<ComputerPart> AllPartsForJoin = new List<ComputerPart>();
        List<ComputerPart>? SelectedProducts = new List<ComputerPart>();
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        public int? AdminId { get; set; }
        CustomerAccount? CurrentCustomer { get; set; }
        public int? CurrentCustomerId { get; set; }

        //arraykey 0 = Q, 1 = W, 2 = E
        //related to selected items list & array
        private static readonly ConsoleKey[] OfferKeys = { ConsoleKey.Q, ConsoleKey.W, ConsoleKey.E };
        public void RenderPage(ApplicationManager applicationManager)
        {
            Console.Clear();
            ConsoleHelper.ResetConsole();
            SetPageCommands();
            PageBanners.DrawShopBanner();

            DrawAccountProfile(applicationManager);

            int left = 2;
            int top = 18;
            int spacing = 55;

            //Just 3 offers, draw them, Math.Min ensures we draw at most 3 and whatever selectproducts is, could be 2, if 4, still draw 3
            for (int i = 0; i < Math.Min(3, SelectedProducts.Count); i++)
            {

                var product = SelectedProducts[i];
                var key = OfferKeys[i];
                var rows = new List<string>
            {
                //Nicer formatting
                $"Id: {product.Id}".PadRight(40),
                product.Name.PadRight(40),
                $"Price: {product.Price} €".PadRight(40),
                (product.Sale ? "ON SALE!" : "(Not on sale)").PadRight(40),
            };
                    //Some text wrapping, longer chunks of texts (like description) gets broken up into chunks by max length
                    foreach (var line in GeneralHelpers.TextWrapper(product.Description, 40))
                    {
                        rows.Add(line.PadRight(40));
                    }
                    var window = new MickesWindow.Window(
                        $"Offer {i + 1}, press {key} to add to cart",
                        left + (i * spacing + 1),
                        top,
                        rows
                    );
                    //Draw X window
                    window.Draw();
                }
        }
        public void DrawAccountProfile(ApplicationManager applicationManager)
        {
            List<string> accountInfo = new List<string>();
            accountInfo = PageAccount.ReturnCustomerProfileAccountString(applicationManager);
            PageAccount.DrawAccountGraphic(accountInfo, "", ConsoleColor.DarkCyan);
            Console.SetCursorPosition(0, 15);
        }
        public async Task<IPage?> HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            if (PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
            {
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
                    case PageControls.PageOption.Checkout:
                        return new CheckoutPage();
                    case PageControls.PageOption.ViewObject:
                        CheckoutObject(applicationLogic);
                        return this;
                    case PageControls.PageOption.AddToBasket:
                        //add to basket stuff
                        AddSomethingToBasket(applicationLogic);
                        return this;
                    case PageControls.PageOption.CustomerLogin:
                        return new CustomerPage();
                }
            }
            //Key to product => Q = First offer
            var keyToProduct = new Dictionary<ConsoleKey, ComputerPart>();
            for (int i = 0; i < SelectedProducts.Count && i < OfferKeys.Length; i++)
            {
                keyToProduct[OfferKeys[i]] = SelectedProducts[i];
            }
            //Ask how many units and confirm
            if (keyToProduct.TryGetValue(UserInput.Key, out var chosenProduct))
            {
                FastBuy(applicationLogic, chosenProduct);
                return this;
            }
            //retunera sida beroende på sida
           

           
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
                   {ConsoleKey.K, PageControls.Checkout },
                   {ConsoleKey.U, PageControls.AddToBasket },
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

        public void Load(ApplicationManager appLol)
        {
            //Random 3 objects every load
            SelectedProducts = appLol.GetFrontPageProducts().OrderBy(_ => Random.Shared.Next(100)).Take(3).ToList(); ;
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
            appLol.RefreshBasket();

        }
        public void CheckoutObject(ApplicationManager app)
        {
            Console.WriteLine("Hello!");
            if (SelectedProducts != null)
            {
                app.CheckoutObject(SelectedProducts);

            }

            //var products = app.GetDBProductsFromList(SelectedProducts);
            //var choosenObject = app.ChooseProductFromList(products);
            //if (choosenObject == null) 
            //{
            //    return;
            //}
            //Console.WriteLine($"Id: {choosenObject.Id} Name; {choosenObject.Name}");

            //var category = app.GetCategory(choosenObject.Id) ?? throw new Exception("Could not find category");
            //var brand = app.GetBrand(choosenObject.Id) ?? throw new Exception("Could not find brand");
            //choosenObject.Read(brand, category);
            //Console.SetCursorPosition(5, 40);
            //Console.WriteLine("Add to basket?");
            //bool yes = GeneralHelpers.ChangeYesOrNo(false);
            //if (!yes)
            //{
            //    app.InformOfQuittingOperation();
            //    return;
            //}
            //if (yes && CurrentCustomer != null)
            //{
            //    app.AddProductToBasket(choosenObject, CurrentCustomerId.Value);
            //}
            //else if (CurrentCustomer == null)
            //{
            //    Console.WriteLine("You need to be logged in to add to basket");
            //    app.InformOfQuittingOperation();
            //}

        }
        public void AddSomethingToBasket(ApplicationManager appLo)
        {
            if (CurrentCustomer == null)
            {
                Console.WriteLine("You have to be logged in");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Please input the corresponding id of the product you wish to add to your basket");
            int choice = GeneralHelpers.StringToInt();
            if (choice == 0)
            {
                Console.WriteLine("Invalid, returning");
                Console.ReadLine();
                return;
            }
            var valid = SelectedProducts.FirstOrDefault(x => x.Id == choice);
            Console.ReadLine();
            if (valid != null)
            {
                appLo.AddProductToBasket(valid, CurrentCustomerId.Value);
            }
            else
            {
                Console.WriteLine("Not valid, returning...");
                Console.ReadLine();
                return;
            }
        }
        public void FastBuy(ApplicationManager appLo, ComputerPart part)
        {
            if (CurrentCustomer == null)
            {
                Console.WriteLine("You have to be logged in");
                Console.ReadLine();
                return;
            }
            var valid =SelectedProducts.FirstOrDefault(x=>x.Id == part.Id);
            if (valid != null)
            {
                appLo.AddProductToBasket(valid, CurrentCustomerId.Value);
            }
            else
            {
                Console.WriteLine("Not valid, returning...");
                Console.ReadLine();
                return;
            }
        }
        public ConsoleKey? GetKeyFor(PageControls.PageOption option)
        {
            return PageCommands
                .FirstOrDefault(p => p.Value.PageCommandOptionInteraction == option)
                .Key;
        }
    }
}
