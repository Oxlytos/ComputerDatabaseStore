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
        static List<string> pageOptions = new List<string> { "[A] for admin page", "","[C] for customer page", "","[H] to go the home page" };
        public void RenderPage()
        {
            Console.Clear();
            Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkGreen);
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Browse product page");
        }

        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
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

        public void PageOptions()
        {
            throw new NotImplementedException();
        }
    }
}
