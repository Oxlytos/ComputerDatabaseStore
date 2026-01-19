using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public class GPU : ComputerPart
    {
        public int? MemoryTypeId { get; set; }

        public virtual MemoryType? MemoryType { get; set; }

        public decimal? GPUFrequency { get; set; }

        public int MemorySizeGB { get; set; }

        public decimal MemorySpeed {  get; set; }

        public bool Overclock {  get; set; }
        public int RecommendedPSUWattage { get; set; }
        public int WattageConsumption { get; set; }

        public GPU() { }

        public override void Create(ApplicationManager lol)
        {
            var vendors = lol.GetVendors();
            var manufacturers = lol.GetManufacturers();
            var memTypes = lol.GetMemoryTypes();
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
            lol.SaveGPU(newGpu);
        }

        public override void Read(ApplicationManager lol)
        {
            throw new NotImplementedException();
        }

        public override void Update(ApplicationManager lol)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ApplicationManager lo)
        {
            throw new NotImplementedException();
        }
    }
}
