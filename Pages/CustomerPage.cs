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
        static List<string> pageOptions = new List<string> { "[B] to browse products", "","[H] to go the home page", "", "[A] for admin page" };
        public void RenderPage()
        {
            Console.Clear();
            Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkGreen);
            Console.SetCursorPosition(0, 10);
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

        public void PageOptions()
        {
            throw new NotImplementedException();
        }
    }
}
