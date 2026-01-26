using ComputerStoreApplication.Crud_Related;
using ComputerStoreApplication.Helpers;
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
        public int? CurrentCustomerId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        public void SetPageCommands()
        {
            //Specific commands per sida
            //Sparar kommandon till tangenter på sidor
            PageCommands = new Dictionary<ConsoleKey, PageControls.PageCommand>
            {
                { ConsoleKey.H, PageControls.HomeCommand },
                { ConsoleKey.B, PageControls.BrowseCommand },
                { ConsoleKey.A, PageControls.Admin },
                {ConsoleKey.N, PageControls.AdminCreateProduct },
                {ConsoleKey.M, PageControls.AdminCreateCategory },
                {ConsoleKey.P, PageControls.AdminCreateStoreProduct }
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
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Admin page");
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
            Console.WriteLine("Hello");
        }
    }
}
