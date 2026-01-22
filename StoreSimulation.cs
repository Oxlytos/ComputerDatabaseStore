using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.MickesWindow;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using ComputerStoreApplication.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
           // FillDatabaseWithBaseInformation();
            //  SetupMenus();
            Console.BackgroundColor = ConsoleColor.Gray;
            MainSimulationLogic();

            Console.ReadLine();
        }
        private static void MainSimulationLogic() 
        {
           
            //Databas connection
            var db = new Logic.ComputerDBContext();
            Brand ban = new Brand
            {
                Name = "Intel",
                
            };
            db.BrandManufacturers.Add(ban);
            db.SaveChanges();

            //Validerings hanterarer innan vi sparar saker och så
            var val = new Logic.ValidationManager();

            //Repon som ringer db och sparar till den
            var rep = new Logic.ComponentRepo(db);

            //Komponenten som hanterar olika services/API kallelser
            var service = new Logic.ComponentService(rep, val);

            //Hanterar sido-logik
            var computerApplicationLogic = new Logic.ApplicationManager(service);
            //Bestäm sidofärg


            while (true) 
            {
                Console.Clear();
                Console.CursorVisible = false;
                //Visa nuvarande sida
                computerApplicationLogic.CurrentPage.Load(computerApplicationLogic);


                computerApplicationLogic.CurrentPage.RenderPage();
                //Knapptryck på denna sida
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                //Gör X sak på sida genom HandleUserInput
            
                var actionOnPage = computerApplicationLogic.CurrentPage.HandleUserInput(consoleKeyInfo, computerApplicationLogic);

                //Existerar sidan, byt till den, gör dens funktioner sedan
                if (actionOnPage != null)
                {
                    computerApplicationLogic.CurrentPage = actionOnPage;
                }

            }
        }

        static void AdminPage()
        {
            Console.WriteLine("Loading admin page...");
            Console.ReadLine();
            Console.Clear();
            PageBanners.DrawShopBanner();
            List<string> adminActions = new List<string> { "Register Product", "Edit Product", "Delete Product", "Add new category" };
            adminActions = GeneralHelpers.ReturnNumberedList(adminActions);
            var adminWindow = new Window("Admin Actions, Press 'A' to choose an action", 2, 4, adminActions);
            adminWindow.Draw();

            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "a")
            {
                Console.WriteLine("What action? Press the corresponding number from the action list");

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Register Product");
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Edit product");
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("Delete a product");
                        break;
                    case ConsoleKey.D4:
                        Console.WriteLine("Add a new category");

                        break;

                }
            }

        }
        static void FillDatabaseWithBaseInformation()
        {
            //CPU arch
            Console.WriteLine("Seeding database with base specs");
            Console.ReadLine();
            using var db = new Logic.ComputerDBContext();
            var x86_64 = new CPUArchitecture { Name = "x86-64" };
            var x86 = new CPUArchitecture { Name = "x86" };
            var x64 = new CPUArchitecture { Name = "x64" };
            var ARM = new CPUArchitecture { Name = "ARM" };
            var ia32 = new CPUArchitecture { Name = "IA-32" };
             //db.AllComponentSpecifcations.AddRange(x86_64, x86, x64, ARM, ia32);

            //CPU socket
            var amdam4 = new CPUSocket { Name = "AMD AM4" };
            var amdam5 = new CPUSocket { Name = "AMD AM5" };
            var amdstr5 = new CPUSocket { Name = "AMD sTR5" };
            var amdswrx8 = new CPUSocket { Name = "AMD sWRX8" };
            var intel1700 = new CPUSocket { Name = "Intel 1700" };
            var intel1851 = new CPUSocket { Name = "Intel 1851" };
          //  db.AllComponentSpecifcations.AddRange(amdam4, amdam5, amdstr5, amdswrx8, intel1700, intel1851);

            //Energy classes
            List<EnergyClass> energyClasses = new List<EnergyClass>();
            energyClasses.Add(new EnergyClass { Name = "80 Plus" });
            energyClasses.Add(new EnergyClass { Name = "80 Plus Bronze" });
            energyClasses.Add(new EnergyClass { Name = "80 Plus Silver" });
            energyClasses.Add(new EnergyClass { Name = "80 Plus Gold" });
            energyClasses.Add(new EnergyClass { Name = "80 Plus Platinum" });
            energyClasses.Add(new EnergyClass { Name = "80 Plus Titanium" });
            energyClasses.Add(new EnergyClass { Name = "80 Plus Ruby" });
           //  db.AllComponentSpecifcations.AddRange(energyClasses);

            //Most different memeory typesd
            List<MemoryType> memoryTypes = new List<MemoryType>();
            memoryTypes.Add(new MemoryType { Name = "DDR3 SODIMM" });
            memoryTypes.Add(new MemoryType { Name = "DDR3L SODIMM" });
            memoryTypes.Add(new MemoryType { Name = "DDR4" });
            memoryTypes.Add(new MemoryType { Name = "DDR4 SODIMM" });
            memoryTypes.Add(new MemoryType { Name = "DDR5 CUDIMM" });
            memoryTypes.Add(new MemoryType { Name = "DDR5 RDIMM" });
            memoryTypes.Add(new MemoryType { Name = "GDDR5"});
            memoryTypes.Add(new MemoryType { Name = "GDDR5X" });
            memoryTypes.Add(new MemoryType { Name = "GDDR6X" });
            memoryTypes.Add(new MemoryType { Name = "GDDR6" });
            memoryTypes.Add(new MemoryType { Name = "GDDR7" });
            memoryTypes.Add(new MemoryType { Name = "GDDR7X" });

         //   db.AllComponentSpecifcations.AddRange(memoryTypes);

            //Base profiles that most use
            List<RamProfileFeatures> ramProfileFeatures = new List<RamProfileFeatures>();
            ramProfileFeatures.Add(new RamProfileFeatures { Name = "XMP" });
            ramProfileFeatures.Add(new RamProfileFeatures { Name = "EXPO" });
            //   db.RamProfiles.AddRange(ramProfileFeatures);

            //maker of chiipsets for cpus, gpus
            List<ChipsetVendor> vendors = new List<ChipsetVendor>();
            vendors.Add(new ChipsetVendor { Name = "Intel" });
            vendors.Add(new ChipsetVendor { Name = "AMD" });
            vendors.Add(new ChipsetVendor { Name = "Nvidia" });
            //  db.Vendors.AddRange(vendors);

            //most brands/manufacturers
            List<Brand> manufacturers = new List<Brand>();
            manufacturers.Add(new Brand { Name = "MSI" });
            manufacturers.Add(new Brand { Name = "ASUS" });
            manufacturers.Add(new Brand { Name = "Acer" });
            manufacturers.Add(new Brand { Name = "Kingston" });
            manufacturers.Add(new Brand { Name = "Corsair" });
            manufacturers.Add(new Brand { Name = "G.Skill" });
            manufacturers.Add(new Brand { Name = "Gigabyte" });
            manufacturers.Add(new Brand { Name = "PowerColor" });
            manufacturers.Add(new Brand { Name = "Sapphire" });
            manufacturers.Add(new Brand { Name = "Sparkle" });
            manufacturers.Add(new Brand { Name = "Cooler Master" });
            manufacturers.Add(new Brand { Name = "Seasonic" });
            db.BrandManufacturers.AddRange(manufacturers);

            List<ChipsetVendor> chipsetVendors = new List<ChipsetVendor>();
            chipsetVendors.Add(new ChipsetVendor { Name = "AMD" });
            chipsetVendors.Add(new ChipsetVendor { Name = "Intel" });
            chipsetVendors.Add(new ChipsetVendor { Name = "Nvidia" });

        //   db.ChipsetVendors.AddRange(chipsetVendors);
            db.SaveChanges();
            Console.WriteLine("Saved changes!");

            /*    GPU rtx4070 = new GPU
                {
                    Name = "MSI GeForce RTX 4070 Super 12GB Ventus 2X OC",
                    ManufacturerId = db.Manufacturers.First(m => m.Name.ToLower() == "msi").Id,
                    VendorId = db.Vendors.First(m => m.Name.ToLower() == "nvidia").Id,
                    MemoryTypeId = db.MemoryTypes.First(m=>m.MemoryTypeName=="GDDR6X").Id,
                    MemorySizeGB = 12,
                    MemorySpeed = 1980,
                    Overclock=true,
                    RecommendedPSUWattage=650,
                    WattageConsumption=220
                };*/

            //  db.GPUs.Add(rtx4070);
            //  db.SaveChanges();

        }
        static void SetupMenus()
        {
            using (var dbContext = new Logic.ComputerDBContext())
            {
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    Console.Clear();
                    PageBanners.DrawShopBanner();
                    List<Type> types = GeneralHelpers.ReturnComputerPartTypes();
                    List<string> customerOptions = GeneralHelpers.AllCategoriesFoundAsStrings(types, true);
                    var customerWindow = new Window("Site Actions", 120, 4, customerOptions);
                    customerWindow.Draw();

                    /*List<string> adminOptions = new List<string> { "1. Administer Products", "2. Administer Categories", "3. Administer Customers", "4. View Statistics (Queries)" };
                    var adminWindow = new Window("Admin Options (Hidden)", 85, 4, adminOptions);
                    adminWindow.Draw();*/

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
}
