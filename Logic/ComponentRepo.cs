using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using Microsoft.EntityFrameworkCore;
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
        public List<RAM> GetRAMs()
        {
            return _dbContext.RAMs.Cast<RAM>().ToList();
        }
        public List<Motherboard> GetMotherboards()
        {
            return _dbContext.Motherboards.Cast<Motherboard>().ToList();
        }
        public List<PSU> GetPSUs()
        {
            return _dbContext.PSUs.Cast<PSU>().ToList();
        }
        public List<ChipsetVendor> GetVendors()
        {
            return _dbContext.ChipsetVendors.Cast<ChipsetVendor>().ToList();
        }
        public List<Brand> GetManufacturers()
        {
            return _dbContext.BrandManufacturers.Cast<Brand>().ToList();
        }
        public List<CPUSocket> GetSockets() 
        {
            return _dbContext.CPUSockets.Cast<CPUSocket>().ToList();
        }
        public List<EnergyClass> GetEnergyClasses()
        {
            return _dbContext.EnergyClasses.ToList();
        }
        public List<CPUArchitecture> GetCPUArchitectures()
        {
            return _dbContext.CPUArchitectures.Cast<CPUArchitecture>().ToList();
        }
        public List<MemoryType> GetMemoryTypes()
        {
            return _dbContext.MemoryTypes.Cast<MemoryType>().ToList();
        }
        public List<RamProfileFeatures> GetRamProfileFeatures()
        {
            return _dbContext.RamProfiles.Cast<RamProfileFeatures>().ToList();
        }
        public List<StoreProduct> GetStoreProducts()
        {
            return _dbContext.StoreProducts.ToList();
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
                cpu.BrandManufacturer = man.FirstOrDefault(s=>s.Id == cpu.BrandId);
                cpu.ChipsetVendor = vendors.FirstOrDefault(s=>s.Id==cpu.ChipsetVendorId);
                cpu.SocketType = sockets.FirstOrDefault(s => s.Id == cpu.SocketId);
                cpu.CPUArchitecture = cpuArchs.FirstOrDefault(s => s.Id == cpu.CPUArchitectureId);
                
            }

            return cpus;
        }
       
        public void SaveNew(StoreProduct product)
        {
            _dbContext.StoreProducts.Add(product);
            TrySaveChanges();
        }
        public void SaveNew(ComputerPart part)
        {
            _dbContext.AllParts.Add(part);
            TrySaveChanges();
        }
        public void SaveNewSpecification(ComponentSpecification spec)
        {
            _dbContext.AllComponentSpecifcations.Add(spec);
             TrySaveChanges();
        }
        public void RemoveComponent(ComputerPart part)
        {
            _dbContext.Remove(part);
            TrySaveChanges();
        }
        public void RemoveSpec(ComponentSpecification spec)
        {
            _dbContext.Remove(spec);
            TrySaveChanges();
        }
        public void SaveNewCPU(CPU cpu)
        {
            _dbContext.CPUs.Add(cpu);
            TrySaveChanges();
        }
        public void SaveNewGPU(GPU gpu)
        {
            _dbContext.GPUs.Add(gpu);
            TrySaveChanges();
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
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Error when updating, {ex.Message}");
                return false;
            }
            catch(DbException ex)
            {
               Console.WriteLine($"Error when trying to save, {ex.Message}");
                return false;
            }
        }
        public void SaveManufacturer(Brand manufacturer) 
        {
            _dbContext.BrandManufacturers.Add(manufacturer);
            Console.WriteLine("Saved manufacturer!");
            Console.ReadLine();
            _dbContext.SaveChanges();
        
        }

    }
}
