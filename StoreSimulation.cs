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
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication
{
    internal class StoreSimulation
    {
        static List<Type> PartTypes = GeneralHelpers.ReturnComputerPartTypes();

        public static void Run()
        {
            Console.WriteLine("Make sure to fullscreen the applicaiton, then press enter");
            Console.ReadLine();
            PartTypes = GeneralHelpers.ReturnComputerPartTypes();
           // FillDatabaseWithBaseInformation();
            //  SetupMenus();

            MainSimulationLogic();

            Console.ReadLine();
        }
        private static void MainSimulationLogic() 
        {
            //Databas connection
            var db = new Logic.ComputerDBContext();

            //Validerings hanterarer innan vi sparar saker och så
            var val = new Logic.ValidationManager();

            //Repon som ringer db och sparar till den
            var rep = new Logic.ComponentRepo(db);

            //Komponenten som hanterar olika services/API kallelser
            var service = new Logic.ComponentService(rep, val);

            //Hanterar sido-logik
            var computerApplicationLogic = new Logic.ApplicationManager(service);

            while (true) 
            {
                Console.Clear();
                //Visa nuvarande sida
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
            GeneralHelpers.LoadSiteGraphics();
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
            using var db = new Logic.ComputerDBContext();
            //archs https://en.wikipedia.org/wiki/Comparison_of_instruction_set_architectures
            var x86_64 = new CPUArchitecture { CPUArchitectureName = "x86-64" };
            var x86 = new CPUArchitecture { CPUArchitectureName = "x86" };
            var x64 = new CPUArchitecture { CPUArchitectureName = "x64" };
            var ARM = new CPUArchitecture { CPUArchitectureName = "ARM" };
            var ia32 = new CPUArchitecture { CPUArchitectureName = "IA-32" };
            // db.CPUArchitectures.AddRange(x86_64, x86, x64, ARM, ia32);

            var amdam4 = new CPUSocket { CPUSocketName = "AMD AM4" };
            var amdam5 = new CPUSocket { CPUSocketName = "AMD AM5" };
            var amdstr5 = new CPUSocket { CPUSocketName = "AMD sTR5" };
            var amdswrx8 = new CPUSocket { CPUSocketName = "AMD sWRX8" };
            var intel1700 = new CPUSocket { CPUSocketName = "Intel 1700" };
            var intel1851 = new CPUSocket { CPUSocketName = "Intel 1851" };
            //  db.CPUSockets.AddRange(amdam4, amdam5, amdstr5, amdswrx8, intel1700, intel1851);

            //
            List<EnergyClass> energyClasses = new List<EnergyClass>();
            energyClasses.Add(new EnergyClass { EnergyNameClass = "80 Plus" });
            energyClasses.Add(new EnergyClass { EnergyNameClass = "80 Plus Bronze" });
            energyClasses.Add(new EnergyClass { EnergyNameClass = "80 Plus Silver" });
            energyClasses.Add(new EnergyClass { EnergyNameClass = "80 Plus Gold" });
            energyClasses.Add(new EnergyClass { EnergyNameClass = "80 Plus Platinum" });
            energyClasses.Add(new EnergyClass { EnergyNameClass = "80 Plus Titanium" });
            energyClasses.Add(new EnergyClass { EnergyNameClass = "80 Plus Ruby" });
            // db.EnergyClasses.AddRange(energyClasses);

            List<MemoryType> memoryTypes = new List<MemoryType>();
            memoryTypes.Add(new MemoryType { MemoryTypeName = "DDR4" });
            memoryTypes.Add(new MemoryType { MemoryTypeName = "DDR5" });
            memoryTypes.Add(new MemoryType { MemoryTypeName = "GDDR5" });
            memoryTypes.Add(new MemoryType { MemoryTypeName = "GDDR6X" });
            memoryTypes.Add(new MemoryType { MemoryTypeName = "GDDR6" });
            memoryTypes.Add(new MemoryType { MemoryTypeName = "GDDR7" });

            // db.MemoryTypes.AddRange(memoryTypes);

            List<RamProfileFeatures> ramProfileFeatures = new List<RamProfileFeatures>();
            ramProfileFeatures.Add(new RamProfileFeatures { RamProfileFeaturesType = "XMP" });
            ramProfileFeatures.Add(new RamProfileFeatures { RamProfileFeaturesType = "EXPO" });
            //   db.RamProfiles.AddRange(ramProfileFeatures);

            List<Vendor> vendors = new List<Vendor>();
            vendors.Add(new Vendor { Name = "Intel" });
            vendors.Add(new Vendor { Name = "AMD" });
            vendors.Add(new Vendor { Name = "Nvidia" });
            //  db.Vendors.AddRange(vendors);

            List<Manufacturer> manufacturers = new List<Manufacturer>();
            manufacturers.Add(new Manufacturer { Name = "MSI" });
            manufacturers.Add(new Manufacturer { Name = "ASUS" });
            manufacturers.Add(new Manufacturer { Name = "Acer" });
            manufacturers.Add(new Manufacturer { Name = "Kingston" });
            manufacturers.Add(new Manufacturer { Name = "Corsair" });
            manufacturers.Add(new Manufacturer { Name = "G.Skill" });
            manufacturers.Add(new Manufacturer { Name = "Gigabyte" });
            //   db.Manufacturers.AddRange(manufacturers);

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
                    GeneralHelpers.LoadSiteGraphics();
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
