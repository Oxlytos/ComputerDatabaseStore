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
        public void RenderPage()
        {
            Console.WriteLine("Customer Account and Info");
        }
        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            if (UserInput.Key == ConsoleKey.B)
            {
                return new BrowseProducts();
            }
            if (UserInput.Key == ConsoleKey.H)
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
