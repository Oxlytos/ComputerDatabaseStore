using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Logic
{
    public class ComponentService
    {
        //Här är steget innan det läggs till i databasen
        //Validera så att sakerna vi lägger till inte redan finns eller är konstigt strukturerat
        //Vi hämtar även information direkt från databasen som vendors, gpus annat
        private readonly ComponentRepo _repo;
        private readonly ValidationManager _validation;

        public ComponentService( ComponentRepo repo, ValidationManager val)
        {
            _repo = repo;
        }
        public void SaveCPU(CPU newCPU)
        {
            //Validate lol
            _validation.Validate("huh");
            _repo.SaveNewCPU(newCPU);
        }
        public void SaveGPU(GPU gpu)
        {
            _repo.SaveNewGPU(gpu);
        }
        public void SaveManufacturer(Models.Vendors_Producers.Manufacturer newMan)
        {
            _validation.Validate("huh");
            _repo.SaveManufacturer(newMan);
        }
        public List<Models.Vendors_Producers.Vendor> GetVendors()
        {
            return _repo.GetVendors();
        }
        public List<Models.Vendors_Producers.Manufacturer> GetManufacturers()
        {
            return _repo.GetManufacturers();
        }
        public List<Models.ComponentSpecifications.CPUSocket> GetCPUSockets()
        {
            return _repo.GetSockets();
        }
        public List<Models.ComponentSpecifications.CPUArchitecture> GetCPUArchitectures()
        {
            return _repo.GetCPUArchitectures();
        }

        public List<Models.ComputerComponents.GPU> GetGPUs()
        {
            return _repo.GetGPUs();
        }
        public List<Models.ComponentSpecifications.MemoryType> GetMemoryTypes() 
        {
            return _repo.GetMemoryTypes();
        }
    }
}
