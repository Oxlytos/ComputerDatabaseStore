using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Crud_Related
{
    public class CreateComponents
    {
        public Dictionary<ConsoleKey, CRUD> keyValuePairs;
        private static readonly Dictionary<ConsoleKey, CRUD> Commandos = new()
        {
            {  ConsoleKey.C, CRUD.Create },
            { ConsoleKey.D, CRUD.Delete },
            { ConsoleKey.U, CRUD.Update },
            {ConsoleKey.R, CRUD.Read },
        };

        public enum CRUD
        {
            Create = ConsoleKey.C, //Skapa product
            Read = ConsoleKey.R, //Läs all info på product => Bara printa upp allt?
            Update = ConsoleKey.U, //Uppdatera som i att vi ändrar värden
            Delete = ConsoleKey.D, //Ta bort, duh
        }
        public static void GetInputs(ApplicationManager logic)
        {
            ComputerPart whatItIs = AskWhatProductType(logic);
            Console.WriteLine("What CRUD action?");
            foreach (var key in Commandos)
            {
                Console.WriteLine($"[{key.Key}] to {key.Value} a product");
            }
            var userInputC = Console.ReadKey(true);
            if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
            {
                Console.WriteLine("Some error here bud");
                return;
            }
            switch (userCrudValue)
            {
                case CRUD.Create:
                    whatItIs.Create(logic);
                    break;
                case CRUD.Read:
                    //logic.get
                    //int whichObjectDoesItAffect;
                    //logic.Printa alla objekt av X typ
                    //få id av rätt, kalla sen logik därifrån
                    Console.WriteLine("Input the corresponding ID as an int in the console please");
                    foreach(var gpu in logic.GetGPUs())
                    {
                        Console.WriteLine($"ID: {gpu.Id} Name: {gpu.Name}");
                    }
                    int whatID = GeneralHelpers.StringToInt(Console.ReadLine());
                    if (whatID != null)
                    {
                        whatItIs.Read(logic, whatID);
                    }
                    break;
                case CRUD.Update:
                    whatItIs.Update(logic, 1);
                    break;
                case CRUD.Delete:
                    whatItIs.Delete(logic, 1);
                    break;
            }
            /*
            Console.WriteLine("What type of product?");
            var vendors = logic.GetVendors();
            var manufacturers = logic.GetManufacturers();

            List<Type> types = GeneralHelpers.ReturnComputerPartTypes();
            for (int i = 0; i < types.Count; i++)
            {
                Console.WriteLine($"{i + 1} {types[i].Name}");
            }

            string usIn = Console.ReadLine();
            if (Int32.TryParse(usIn, out int choice))
            {
                choice -= 1;
                switch (choice)
                {
                    case 0: //CPU
                        Console.WriteLine("Starting creation process and form for CPU...");
                        var sockets = logic.GetCPUSockets();
                        var archs = logic.GetCPUArchitectures();
                        CPU chudCPU = CreateComponents.RegisterNewCPU(vendors, manufacturers, sockets, archs);
                        logic.SaveCPU(chudCPU);
                        Console.ReadLine();
                        break;
                    case 1:
                        //GPU
                        Console.WriteLine("Starting creation process and form for GPU...");
                        var memoryTypes = logic.GetMemoryTypes();
                        GPU newGPU = CreateComponents.RegisterNewGPU(vendors, manufacturers, memoryTypes);
                        logic.SaveGPU(newGPU);
                        break;
                    case 2:
                        Console.WriteLine("Creating PSU form...");
                        break;
                    case 3:
                        Console.WriteLine("Bongos");
                        break;
                    case 4:
                        Console.WriteLine("Bongos");
                        break;
                    case 5:
                        Console.WriteLine("Bongos");
                        break;
                    case 6:
                        Console.WriteLine("Bongos");
                        break;

                }
            }*/
        }
        static void CreateProduct(CPU component, ApplicationManager logic)
        {
            Console.WriteLine("What type of product do you wanna create/register?");
            ComputerPart what = AskWhatProductType(logic);
        }
        static void CreateProduct(GPU component, ApplicationManager logic)
        {

        }
        static void ReadProduct(ApplicationManager logic)
        {
            AskWhatProductType(logic);
        }
        static void UpdateProduct(ApplicationManager logic)
        {
            AskWhatProductType(logic);
        }
        static void DeleteProduct(ApplicationManager logic)
        {
            AskWhatProductType(logic);
        }

        static ComputerPart AskWhatProductType(ApplicationManager logic)
        {
            Console.WriteLine("What type of product?");
            List<Type> types = GeneralHelpers.ReturnComputerPartTypes();
            for (int i = 0; i < types.Count; i++)
            {
                Console.WriteLine($"{i + 1} {types[i].Name}");
            }

            string usIn = Console.ReadLine();
            if (Int32.TryParse(usIn, out int choice))
            {
                choice -= 1;
                switch (choice)
                {
                    case 0:
                        return new CPU();
                    case 1:
                        return new GPU();
                    case 2:
                        return new Motherboard();
                    case 3:
                        return new PSU();
                    case 4:
                        return new RAM();
                }
            }
            return null;
        }
        static void LoadBackgroundForm()
        {
            int startPosX = GeneralHelpers.ReturnMiddleOfTheScreenXAxisWithOffsetForSomeStringOrLength(15);
            Helpers.WindowStuff.EmptyWindow form = new Helpers.WindowStuff.EmptyWindow
            {
                Header = " Questionnaire",
                Left = startPosX,
                Top = 15,
                Height = 25,
                BgColor = ConsoleColor.Red,
            };
            form.Draw();
        }
        public static CPU RegisterNewCPU(List<Vendor> vendors, List<Manufacturer> manufacturers, List<CPUSocket> socks, List<CPUArchitecture> archs)
        {
            Console.WriteLine("Name of the CPU?");
            string CPUName = Console.ReadLine();

            Vendor vendor = GeneralHelpers.ChooseVendor(vendors);
            Manufacturer manufacturer = GeneralHelpers.ChooseManufacturer(manufacturers);
            CPUSocket socket = GeneralHelpers.ChooseCPUSocket(socks);
            CPUArchitecture arch = GeneralHelpers.ChooseCPUArch(archs);

            Console.WriteLine("Amount of cores?");
            int cores = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("Threadcount?");
            int threads = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("Clock speed (GHz)?");
            decimal clockSpeed = GeneralHelpers.StringToDecimal(Console.ReadLine());

            Console.WriteLine("Overclockable? Type (Y) for yes, (N) for no, then press 'Enter'");
            bool overclockable = GeneralHelpers.YesOrNoReturnBoolean(Console.ReadLine());

            Console.WriteLine("How many we got in stock of this new CPU?");
            int stock = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("How much CPU cache?");
            decimal cach = GeneralHelpers.StringToDecimal(Console.ReadLine());

            CPU newCPU = new CPU
            {
                Name = CPUName,

                Vendor = vendor,
                VendorId = vendor.Id,
                Manufacturer = manufacturer,
                ManufacturerId = manufacturer.Id,

                SocketType = socket,
                SocketId = socket.Id,
                CPUArchitecture = arch,
                CPUArchitectureId = arch.Id,

                Cores = cores,
                Threads = threads,
                MemorySpeedGhz = clockSpeed,
                Overclockable = overclockable,
                Stock = stock,
                CPUCache = cach

            };

            return newCPU;
        }
        public static GPU RegisterNewGPU(List<Vendor> vendors, List<Manufacturer> manufacturers, List<MemoryType> memTypes)
        {
            Console.WriteLine("Name?");
            string gpuName = Console.ReadLine();

            Vendor vendor = GeneralHelpers.ChooseVendor(vendors);
            Manufacturer manufacturer = GeneralHelpers.ChooseManufacturer(manufacturers);
            MemoryType whatMemoryType = GeneralHelpers.ChooseMemoryType(memTypes);

            Console.WriteLine("Memory speed? (MHz)");
            decimal memorySpeed = GeneralHelpers.StringToDecimal(Console.ReadLine());

            Console.WriteLine("How many GBs?");
            int gbs = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("GPU frequency (MHz)?");
            decimal freqSpeed = GeneralHelpers.StringToDecimal(Console.ReadLine());

            Console.WriteLine("Overclockable? Type (Y) for yes, (N) for no, then press 'Enter'\"");
            bool overclock = GeneralHelpers.YesOrNoReturnBoolean(Console.ReadLine());

            Console.WriteLine("Recommended PSU Wattage?");
            int psuPower = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("Wattage consumption?");
            int wConsumption = GeneralHelpers.StringToInt(Console.ReadLine());

            GPU newGpu = new GPU
            {
                Name = gpuName,
                Vendor = vendor,
                VendorId = vendor.Id,
                Manufacturer = manufacturer,
                ManufacturerId = manufacturer.Id,
                MemoryType = whatMemoryType,
                MemoryTypeId = whatMemoryType.Id,

                MemorySpeed = memorySpeed,
                MemorySizeGB = gbs,
                GPUFrequency = freqSpeed,

                Overclock = overclock,
                RecommendedPSUWattage = psuPower,
                WattageConsumption = wConsumption,
            };
            return newGpu;

        }
    }
}
