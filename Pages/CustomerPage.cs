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
            Helpers.GeneralHelpers.LoadSiteGraphics();
        }
        public IPage? HandleUserInput(ConsoleKeyInfo UserInput)
        {
            return null;
        }
    }
}
