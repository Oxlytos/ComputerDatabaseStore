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
        private readonly ComponentRepo _repo;
        private readonly ValidationManager _validation;

        public ComponentService( ComponentRepo repo, ValidationManager val)
        {
            _repo = repo;
            _validation = val;
        }
        public void SaveCPU(CPU newCPU)
        {
            //Validate lol
            _validation.Validate("huh");
            _repo.SaveNewCPU(newCPU);
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
    }
}
