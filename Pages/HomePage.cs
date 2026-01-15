using ComputerStoreApplication.Graphics;
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
        static List<string> pageOptions = new List<string> { "[A] for admin page", "","[C] for customer page", "","[B] to browse products" };
        public void RenderPage()
        {
            Console.Clear();
            Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkGreen);
            PageBanners.DrawShopBanner();
            Console.SetCursorPosition(0, 10);
            
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

        public void PageOptions()
        {
            throw new NotImplementedException();
        }
    }
}
