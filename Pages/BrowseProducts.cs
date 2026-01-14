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
        public void RenderPage()
        {
            Console.WriteLine("Browse product page");
        }

        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationLogic applicationLogic)
        {
            if (UserInput.Key == ConsoleKey.C) 
            {
                return new CustomerPage();
            }
            if(UserInput.Key == ConsoleKey.H)
            {
                return new HomePage();
            }
            if (UserInput.Key == ConsoleKey.A) 
            {
                return new AdminPage();
            }
            return null;
        }

    }
}
