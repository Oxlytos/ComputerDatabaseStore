using ComputerStoreApplication.MickesWindow;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication
{
    internal class StoreSimulation
    {
        public static void Run()
        {
                Console.WriteLine("Make sure to fullscreen the applicaiton, then press enter");

                Console.ReadLine();
                CreateComponents();
                SetupMenus();

                Console.ReadLine();
        }

        static void CreateComponents()
        {
            using var db = new ComputerDBContext();

            var amdVendor = db.Vendors.FirstOrDefault(v => v.Name == "AMD");
            if (amdVendor == null)
            {
                amdVendor = new Vendor { Name = "AMD" };
                db.Vendors.Add(amdVendor);
            }

            CPU amdExampleProcessor = new CPU
            {
                Name = "AMD Ryzen 7 7800X3D",
                Vendor = amdVendor,
                Cores = 8,
                Threads = 16,
                CPUCache = 96m
            };

            db.CPUs.Add(amdExampleProcessor);
            db.SaveChanges();
                
        }
        static void SetupMenus()
        {
            using (var dbContext = new ComputerDBContext()) 
            {
                Console.Clear();
                List<string> pageHeader = new List<string> { "•ᴗ• Oscar's Computer Store •ᴗ•", "Most Things PC related :^)" };
                var windowTop1 = new Window("", 2, 0, pageHeader);
                windowTop1.Draw();

                List<string> customerOptions = new List<string> { "1. Startpage", "2. Store Supply", "3. Shopping Cart" };
                var customerWindow = new Window("Customer Page Options", 120, 4, customerOptions);
                customerWindow.Draw();

                List<string> adminOptions = new List<string> { "1. Administer Products", "2. Administer Categories", "3. Administer Customers", "4. View Statistics (Queries)" };
                var adminWindow = new Window("Admin Options (Hidden)", 85, 4, adminOptions);
                adminWindow.Draw();

                var newWindow = new Helpers.WindowStuff.WideWindow("Categories", 2, 4, customerOptions);
                newWindow.Draw();

                List<string> topText2 = new List<string> { "AMD 7800x3d", "THE gaming processor", "Price: 450€", "Press (A) to buy" };
                var windowTop2 = new Window("Offer 1", 2, 10, topText2);
                windowTop2.Draw();

                //List<string> topText3 = new List<string> { CPUs.First().Id.ToString(), CPUs.First().Name, CPUs.First().Cores.ToString() };
               // var windowTop3 = new Window("Erbjudande 2", 28, 10, topText3);
               /// windowTop3.Draw();

                List<string> topText4 = new List<string> { "Läderskor", "Extra flotta", "Pris: 450 kr", "Tryck C för att köpa" };
                var windowTop4 = new Window("Erbjudande 3", 56, 10, topText4);
                windowTop4.Draw();


            }
           
        }

       


    }
}
