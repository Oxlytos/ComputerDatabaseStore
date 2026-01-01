using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using WindowDemo;

namespace ComputerStoreApplication
{
    internal class StoreSimulation
    {
        public static void Run()
        {
            Console.ReadLine();
            SetupMenus();

            Console.ReadLine();
        }

        static void SetupMenus()
        {
            List<string> pageHeader = new List<string> { "# Oscar's Computer Store #", "Most Things PC related :^)" };
            var windowTop1 = new Window("", 0, 0, pageHeader);
            windowTop1.Draw();

            List<string> customerOptions = new List<string> {"1. Startpage", "2. Store Supply", "3. Shopping Cart" };
            var customerWindow = new Window("Customer Page Options", 120,4, customerOptions);
            customerWindow.Draw();

            List<string> adminOptions = new List<string> { "1. Administer Products", "2. Administer Categories", "3. Administer Customers", "4. View Statistics (Queries)" };
            var adminWindow = new Window("Admin Options (Hidden)", 85,4, adminOptions);
            adminWindow.Draw();

            var newWindow = new Helpers.WindowStuff.WideWindow("Categories", 0, 4, customerOptions);
            newWindow.Draw();
        }

       


    }
}
