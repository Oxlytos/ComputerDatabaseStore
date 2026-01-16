using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public class CPU : ComputerPart
    {
        //https://stackoverflow.com/questions/5542864/how-should-i-declare-foreign-key-relationships-using-code-first-entity-framework

        //https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwith-nrt

        public decimal? MemorySpeedGhz { get; set; }

        public int? Cores { get; set; }

        public int? Threads { get; set; }

        public int? SocketId { get; set; }
        public virtual CPUSocket? SocketType { get; set; }

        public int? CPUArchitectureId { get; set; }
        public virtual CPUArchitecture? CPUArchitecture { get; set; }

        public bool Overclockable { get; set; }

        public decimal? CPUCache { get; set; }

        public CPU() { }

        public override void Create(ApplicationManager lol)
        {
            var vendors = lol.GetVendors();
            var manufacturers = lol.GetManufacturers();
            var sockets = lol.GetCPUSockets();
            var archs = lol.GetCPUArchitectures();

            Console.WriteLine("Name of the CPU?");
            string CPUName = Console.ReadLine();

            Vendor vendor = GeneralHelpers.ChooseVendor(vendors);
            Manufacturer manufacturer = GeneralHelpers.ChooseManufacturer(manufacturers);
            CPUSocket socket = GeneralHelpers.ChooseCPUSocket(sockets);
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

            lol.SaveCPU(newCPU);
        }

        public override void Read(ApplicationManager lol, int id)
        {
            var propertiers = this.GetType().GetProperties();
            Console.WriteLine($"Info on this {this.GetType()} {this.Name}");
            foreach (var prop in propertiers) 
            {
                var propertyValue = prop.GetValue(this);
                Console.WriteLine($"{prop.Name} : {propertyValue}");
            }
        }

        public override void Update(ApplicationManager lol, int id)
        {
            Console.WriteLine("Update name? Leave empty to not change");
        }

        public override void Delete(ApplicationManager lol, int id)
        {
        }
    }
}
