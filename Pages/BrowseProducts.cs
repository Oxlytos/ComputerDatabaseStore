using ComputerStoreApplication.Logic;
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
        public void RenderPage()
        {
            Console.Clear();
            SetPageCommands();
            Graphics.PageBanners.DrawBrowsePageBanner();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Browse product page");
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
                    return new CustomerPage();
                case PageControls.PageOption.AdminPage:
                    return new AdminPage();
                case PageControls.PageOption.Browse:
                    return this;
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
                { ConsoleKey.A, PageControls.Admin },
                {ConsoleKey.B, PageControls.BrowseCommand},
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
