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

namespace ComputerStoreApplication.CreateStuff
{
    public class CreateComponents
    {
        public static CPU RegisterNewCPU(List<Vendor> vendors, List<Manufacturer> manufacturers, List<CPUSocket> socks, List<CPUArchitecture> archs)
        {
            Console.WriteLine("Name?");
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
                CPUCache=cach
                
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
            decimal freqSpeed= GeneralHelpers.StringToDecimal(Console.ReadLine());


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
