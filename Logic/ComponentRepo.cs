using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ComputerStoreApplication.Logic
{
    public class ComponentRepo
    {
        //Här sparar vi till databasen
        //Och hämtar saker från dbn
        //Här ska saker varit validerade innan något sparas
        readonly ComputerDBContext _dbContext;

        public ComponentRepo(ComputerDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        //APU kallelser här nästan
        public List<ComputerPart?> GetAllProducts()
        {
            //Cast:a alla object som ComputerPart, concat till ett lång mixad lista med objekt av samma basklass
           return  _dbContext.CPUs.Cast<ComputerPart>()
                .Concat(_dbContext.GPUs)
                .Concat(_dbContext.RAMs)
                .Concat (_dbContext.PSUs)
                .Concat(_dbContext.Motherboards).
                ToList();
        }

        public List<Vendor> GetVendors()
        {
            return _dbContext.Vendors.Cast<Vendor>().ToList();
        }
        public List<Manufacturer> GetManufacturers()
        {
            return _dbContext.Manufacturers.Cast<Manufacturer>().ToList();
        }
        public List<CPUSocket> GetSockets() 
        {
            return _dbContext.CPUSockets.Cast<CPUSocket>().ToList();
        }
        public List<CPUArchitecture> GetCPUArchitectures()
        {
            return _dbContext.CPUArchitectures.Cast<CPUArchitecture>().ToList();
        }
        public List<MemoryType> GetMemoryTypes()
        {
            return _dbContext.MemoryTypes.Cast<MemoryType>().ToList();
        }
        public void SaveNewCPU(CPU cpu)
        {
            _dbContext.CPUs.Add(cpu);
            _dbContext.SaveChanges();
        }
        public void SaveNewGPU(GPU gpu)
        {
            _dbContext.GPUs.Add(gpu);
            _dbContext.SaveChanges();
        }
        public void SaveManufacturer(Manufacturer manufacturer) 
        {
            _dbContext.Manufacturers.Add(manufacturer);
            Console.WriteLine("Saved manufacturer!");
            Console.ReadLine();
            _dbContext.SaveChanges();
        
        }

    }
}
