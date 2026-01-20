using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        public List<GPU> GetGPUs()
        {
            return _dbContext.GPUs.ToList();
        }
        public List<CPU> GetCPUs()
        {
            var cpus = _dbContext.CPUs.ToList();

            //Koppla ihop virtuella proprterties här
            var man = GetManufacturers();
            var vendors = GetVendors();
            var sockets = GetSockets();
            var cpuArchs = GetCPUArchitectures();
            foreach (var cpu in cpus) 
            {
                cpu.Manufacturer = man.FirstOrDefault(s=>s.Id == cpu.ManufacturerId);
                cpu.Vendor = vendors.FirstOrDefault(s=>s.Id==cpu.VendorId);
                cpu.SocketType = sockets.FirstOrDefault(s => s.Id == cpu.SocketId);
                cpu.CPUArchitecture = cpuArchs.FirstOrDefault(s => s.Id == cpu.CPUArchitectureId);
                
            }

            return cpus;
        }
        public void SaveNew(ComputerPart part)
        {
            _dbContext.AllParts.Add(part);
            bool check = TrySaveChanges();
            if (check) 
            {
                Console.WriteLine("Managed to save new part to database, press enter to continue");
                Console.ReadLine();
            }
            
        }
        public void RemoveComponent(ComputerPart part)
        {
            _dbContext.Remove(part);
            bool check = TrySaveChanges();
            if (check) 
            {
                Console.WriteLine("Managed to remove part from database, press enter to continue");
                Console.ReadLine();
            }
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
        public bool TrySaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
                Console.WriteLine("DB Operation succesfull, press enter to continue");
                Console.ReadLine(); 
                return true;
            }
            catch (DbException ex)
            {
                Console.WriteLine($"Error when saving, {ex.Message}");
                return false;
            }
          
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
