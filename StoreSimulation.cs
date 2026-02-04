using ComputerStoreApplication.Account;
using ComputerStoreApplication.Graphics;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.MickesWindow;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using ComputerStoreApplication.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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
        public static async Task Run()
        {
            //await CheckMongoConnection();
            //For when empty
           // FillDatabaseWithBaseInformation();
            await MainSimulationLogic();
            Console.ReadLine();
        }
        private static async Task CheckMongoConnection()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<ComputerDBContext>().Build();
            var loginPassword = config["MongoPassword"];

            string connectionUri = $"mongodb+srv://oscardbuser:{loginPassword}@cluster0.pb6h2cm.mongodb.net/?appName=Cluster0";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
        private static async Task MainSimulationLogic() 
        {
            //Database connection
            var db = new Logic.ComputerDBContext();
            var config = new ConfigurationBuilder().AddUserSecrets<ComputerDBContext>().Build();
            var loginPassword = config["Password"];
            // optionsBuilder.UseSqlServer($@"Server=tcp:oscardbassigment.database.windows.net,1433;Initial Catalog=ComputerShopDbOscar;Persist Security Info=False;User ID=dbadmin;Password={loginPassword};MultipleActiveResultSets=False; Encrypt=True;TrustServerCertificate=False; Connection Timeout=30;");
             string connstring = ($@" Server =tcp:oscarsdb.database.windows.net,1433;Initial Catalog=oscarcomputershopdb;Persist Security Info=False;User ID=superadmin;Password={loginPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            
            IConnectionFactory connectionFactory = new SQLConnectionFactory(connstring);

            IDapperService dapperService = new DapperService(connectionFactory);
            
            
            
            if (!db.Admins.Any())
            {
                var admin = new AdminAccount
                {
                    UserName = "admin1",
                    FirstName = "Admin",
                    SurName = "Adminsson",
                    Email = "admin.oscar@gmail.com",
                    PhoneNumber = CustomerHelper.RandomPhoneNumber()
                };
                admin.ChangeOwnPassword("1234");
                db.Add(admin);
                db.SaveChanges();
            }
            //Repo often returns basic queries and ToLists()
            var rep = new Logic.ComponentRepo(db);
            //Call service and do basic data handling
            var service = new Logic.ComponentService(rep);
            var mongo = new MongoConnection();
            //Application logic follows between pages, and carries db context which is the main component
            var dbContextFactory = new ContextFactory();
            var computerApplicationLogic = new Logic.ApplicationManager(service, mongo, dbContextFactory, dapperService);
            while (true) 
            {
                Console.Clear();
                Console.CursorVisible = false;
                //Render current choosen page => Which changes later
                //At default load up home page
                computerApplicationLogic.CurrentPage.Load(computerApplicationLogic);
                //Render X page on and on again until application closses
                computerApplicationLogic.CurrentPage.RenderPage(computerApplicationLogic);
                //Key press on a site => Different actions/methods
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                //Handle input
                var actionOnPage = computerApplicationLogic.CurrentPage.HandleUserInput(consoleKeyInfo, computerApplicationLogic);
                //Do thing on page if there's a command for it
                if (actionOnPage != null)
                {
                    //peform action, otherwise it just reloads page
                    
                    computerApplicationLogic.CurrentPage = await actionOnPage;
                    computerApplicationLogic.RefreshBasket();
                }
            }
        }
        static void FillDatabaseWithBaseInformation()
        {
            //    //CPU arch
            //    Console.WriteLine("Seeding database with base specs");
            //    Console.ReadLine();
            using var db = new Logic.ComputerDBContext();
            //var x86_64 = new CPUArchitecture { Name = "x86-64" };
            //var x86 = new CPUArchitecture { Name = "x86" };
            //var x64 = new CPUArchitecture { Name = "x64" };
            //var ARM = new CPUArchitecture { Name = "ARM" };
            //var ia32 = new CPUArchitecture { Name = "IA-32" };
            //db.AllComponentSpecifcations.AddRange(x86_64, x86, x64, ARM, ia32);

            ////CPU socket
            ////var amdam4 = new CPUSocket { Name = "AMD AM4" };
            ////var amdam5 = new CPUSocket { Name = "AMD AM5" };
            ////var amdstr5 = new CPUSocket { Name = "AMD sTR5" };
            ////var amdswrx8 = new CPUSocket { Name = "AMD sWRX8" };
            ////var intel1700 = new CPUSocket { Name = "Intel 1700" };
            ////var intel1851 = new CPUSocket { Name = "Intel 1851" };
            ////db.AllComponentSpecifcations.AddRange(amdam4, amdam5, amdstr5, amdswrx8, intel1700, intel1851);
            //db.SaveChanges();

            //Energy classes
            //List<EnergyClass> energyClasses = new List<EnergyClass>();
            //energyClasses.Add(new EnergyClass { Name = "80 Plus" });
            //energyClasses.Add(new EnergyClass { Name = "80 Plus Bronze" });
            //energyClasses.Add(new EnergyClass { Name = "80 Plus Silver" });
            //energyClasses.Add(new EnergyClass { Name = "80 Plus Gold" });
            //energyClasses.Add(new EnergyClass { Name = "80 Plus Platinum" });
            //energyClasses.Add(new EnergyClass { Name = "80 Plus Titanium" });
            //energyClasses.Add(new EnergyClass { Name = "80 Plus Ruby" });
            //db.AllComponentSpecifcations.AddRange(energyClasses);
            //db.AddRange(energyClasses);
            //db.SaveChanges();

            //Most different memeory typesd
            //List<MemoryType> memoryTypes = new List<MemoryType>();
            //memoryTypes.Add(new MemoryType { Name = "DDR3 SODIMM" });
            //memoryTypes.Add(new MemoryType { Name = "DDR3L SODIMM" });
            //memoryTypes.Add(new MemoryType { Name = "DDR4" });
            //memoryTypes.Add(new MemoryType { Name = "DDR4 SODIMM" });
            //memoryTypes.Add(new MemoryType { Name = "DDR5 CUDIMM" });
            //memoryTypes.Add(new MemoryType { Name = "DDR5 RDIMM" });
            //memoryTypes.Add(new MemoryType { Name = "GDDR5" });
            //memoryTypes.Add(new MemoryType { Name = "GDDR5X" });
            //memoryTypes.Add(new MemoryType { Name = "GDDR6X" });
            //memoryTypes.Add(new MemoryType { Name = "GDDR6" });
            //memoryTypes.Add(new MemoryType { Name = "GDDR7" });
            //memoryTypes.Add(new MemoryType { Name = "GDDR7X" });

            //db.AllComponentSpecifcations.AddRange(memoryTypes);
            //db.SaveChanges();
            //Base profiles that most use
            //List<RamProfileFeatures> ramProfileFeatures = new List<RamProfileFeatures>();
            //ramProfileFeatures.Add(new RamProfileFeatures { Name = "XMP" });
            //ramProfileFeatures.Add(new RamProfileFeatures { Name = "EXPO" });
            //   db.RamProfiles.AddRange(ramProfileFeatures);
            //db.SaveChanges();

            //    //maker of chiipsets for cpus, gpus
            //    List<ChipsetVendor> vendors = new List<ChipsetVendor>();
            //    vendors.Add(new ChipsetVendor { Name = "Intel" });
            //    vendors.Add(new ChipsetVendor { Name = "AMD" });
            //    vendors.Add(new ChipsetVendor { Name = "Nvidia" });
            //    //  db.Vendors.AddRange(vendors);

            ////    //most brands/manufacturers
            List<Brand> manufacturers = new List<Brand>();
            manufacturers.Add(new Brand { Name = "MSI" });
            manufacturers.Add(new Brand { Name = "ASUS" });
            manufacturers.Add(new Brand { Name = "Intel" });
            manufacturers.Add(new Brand { Name = "Nvidia" });
            manufacturers.Add(new Brand { Name = "AMD" });
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

            List<PaymentMethod> pays = new List<PaymentMethod>();
            pays.Add(new PaymentMethod { Name = "Swish" });
            pays.Add(new PaymentMethod { Name = "Klarna" });
            pays.Add(new PaymentMethod { Name = "MasterCard" });
            pays.Add(new PaymentMethod { Name = "Paypal" });
            db.AddRange(pays);
            

            List<DeliveryProvider> deliveryProviders = new List<DeliveryProvider>();
            deliveryProviders.Add(new DeliveryProvider { Name = "PostNord", Price = 10, AverageDeliveryTime = 2 });
            deliveryProviders.Add(new DeliveryProvider { Name = "ExpressBud", Price = 20, AverageDeliveryTime = 1 });
            deliveryProviders.Add(new DeliveryProvider { Name = "Schenker", Price = 12, AverageDeliveryTime = 3 });
            deliveryProviders.Add(new DeliveryProvider { Name = "DHL", Price = 6, AverageDeliveryTime = 2 });
            db.AddRange(deliveryProviders);

            List<ComponentCategory> componentCategories = new List<ComponentCategory>();
            componentCategories.Add(new ComponentCategory { Name = "CPU" });
            componentCategories.Add(new ComponentCategory { Name = "GPU" });
            componentCategories.Add(new ComponentCategory { Name = "PSU" });
            componentCategories.Add(new ComponentCategory { Name = "RAM" });
            componentCategories.Add(new ComponentCategory { Name = "Motherboard" });
            componentCategories.Add(new ComponentCategory { Name = "RAM" });
            db.AddRange(componentCategories);
            //db.SaveChanges();
            Console.WriteLine("Saved changes!");
        }




    }
}
