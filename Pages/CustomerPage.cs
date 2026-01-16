using ComputerStoreApplication.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public class CustomerPage : IPage
    {
        public Dictionary<ConsoleKey, PageControls.PageCommand> PageCommands;
        public void RenderPage()
        {
            Console.Clear();
            SetPageCommands();
            Graphics.PageBanners.DrawCustomerPage();
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
                //Kolla page commands metoden
                case PageControls.PageOption.Home:
                    return new HomePage();
                case PageControls.PageOption.CustomerPage:
                    return this;
                case PageControls.PageOption.AdminPage:
                    return new AdminPage();
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
                { ConsoleKey.A, PageControls.Admin },
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
