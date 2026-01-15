using ComputerStoreApplication.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public class HomePage : IPage
    {
        public void RenderPage()
        {
            Helpers.GeneralHelpers.LoadSiteGraphics();
        }
        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            if (UserInput.Key == ConsoleKey.C)
            {
                return new CustomerPage();
            }
            if (UserInput.Key == ConsoleKey.B)
            {
                return new BrowseProducts();
            }
            if (UserInput.Key == ConsoleKey.A)
            {
                return new AdminPage();
            }
            return null;
        }

      
    }
}
