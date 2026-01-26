using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComputerStoreApplication.Pages.PageControls;

namespace ComputerStoreApplication.Pages
{
    internal class CheckoutPage : IPage
    {
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;

        Customer? CurrentCustomer { get; set; }
        public int? CurrentCustomerId { get; set; }

        List<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();
        List<StoreProduct> StoreProducts { get; set; }

        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //har vi inte deras input
            if (!PageCommands.TryGetValue(UserInput.Key, out var whateverButtonUserPressed))
                return this; //retunera samma sida igen

            //retunera sida beroende på sida
            switch (whateverButtonUserPressed.PageCommandOptionInteraction)
            {
                //Bokstaven N är skapa ny produkt, vi laddar om samma sida, fast kallar en metod innan
                case PageControls.PageOption.AdminCreateComponent:
                    Crud_Related.CrudHandler.ComponentInput(applicationLogic);
                    return this; //This blir denna sida
                case PageControls.PageOption.AdminCreateCategory:
                    Crud_Related.CrudHandler.CategoryInput(applicationLogic);
                    return this;
                case PageControls.PageOption.AdminCreateStoreProduct:
                    Crud_Related.CrudHandler.StoreProductInput(applicationLogic);
                    return this;
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerPage:
                    return new CustomerPage();
                case PageControls.PageOption.Browse:
                    return new BrowseProducts();
            }
            ;
            return this;

        }

        public void Load(ApplicationManager appLol)
        {
            if (!appLol.IsLoggedInAsCustomer)
            {
                CurrentCustomerId = null;
                CurrentCustomer = null;
                Console.WriteLine("Can't access without being logged in");
                Console.ReadLine();
                appLol.CurrentPage = new HomePage();
                return;
            }
            //om inloggad, hämta nnuvarande kund
            CurrentCustomerId = appLol.CustomerId;
            CurrentCustomer = appLol.GetCustomerInfo(appLol.CustomerId);
            var basketProducts = appLol.GetBasketProductsFromCustomerId(CurrentCustomer.Id);
            StoreProducts = appLol.GetStoreProducts();

            foreach (var product in basketProducts)
            {
                product.Product = StoreProducts.FirstOrDefault(x => x.Id == product.ProductId);
            }

            BasketProducts = basketProducts;
        }

        public void RenderPage()
        {
            Graphics.PageBanners.DrawCheckoutPage();
            SetPageCommands();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine($"Logged in as {CurrentCustomer.FirstName} {CurrentCustomer.SurName}");
            if (BasketProducts.Count > 0) 
            {
                foreach (var basketItem in BasketProducts)
                {
                    Console.WriteLine($"Product: {basketItem.Product.Name} €: {basketItem.Product.Price} Quantity {basketItem.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("Empty basket");
            }
           
        }

        public void SetPageCommands()
        {
            //Specific commands per sida
            //Sparar kommandon till tangenter på sidor
            PageCommands = new Dictionary<ConsoleKey, PageControls.PageCommand>
            {
                { ConsoleKey.H, PageControls.HomeCommand },
                {ConsoleKey.C, PageControls. CustomerHomePage},
                {ConsoleKey.B, PageControls.BrowseCommand},
                {ConsoleKey.F, PageControls.Search},
              
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
