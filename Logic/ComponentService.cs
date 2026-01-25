using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        //Fältet var static, kan inte vara om det ska connectad till resten
        public IEnumerable<ComputerPart> GetObjectsOfTheSameType(ComputerPart part)
        {
            return part switch
            {
                GPU=>_repo.GetGPUs(),
                CPU=> _repo.GetCPUs(),
                RAM=>_repo.GetRAMs(),
                Motherboard =>_repo.GetMotherboards(),
                PSU =>_repo.GetPSUs(),


                _ => throw new ArgumentException("Unknown computer part type")

            };
        }
        public IEnumerable<ComponentSpecification> GetSpecsOTheSameType(ComponentSpecification spec)
        {
            return spec switch
            {
                CPUArchitecture => _repo.GetCPUArchitectures(),
                CPUSocket => _repo.GetSockets(),
                EnergyClass => _repo.GetEnergyClasses(),
                MemoryType => _repo.GetMemoryTypes(),
                RamProfileFeatures => _repo.GetRamProfileFeatures(),

                _ => throw new ArgumentException("Unknown specifcation category type")
            };
        }
        public List<Customer> GetCustomers()
        {
            return _repo.GetCustomers();
        }
        public void LoginAdmin()
        {

        }
        public void LogoutAdmin()
        {

        }
        public Customer GetCustomerInfo()
        {
            return null;
        }
        public void LoginCustomer()
        {

        }
        public void LogoutCustomer()
        {

        }
        public List<StoreProduct> GetStoreProducts()
        {
            return _repo.GetStoreProducts();
        }
        public void SaveNew(StoreProduct storeProduct)
        {
            _repo.SaveNew(storeProduct);
        }
        public void SaveNew(ComputerPart part)
        {
            _repo.SaveNew(part);
        }
        public bool SaveChangesOnComponent()
        {
                bool status = _repo.TrySaveChanges();
                return status;
        }
        public void RemoveComponent(ComputerPart part)
        {
            _repo.RemoveComponent(part);
        }
        public void  SaveNewCustomer(Customer cus)
        {
            _repo.SaveNewCustomer(cus);
        }
        public void AddProductToBasket(BasketProduct basketProduct, Customer cus)
        {
            _repo.AddProductToBasket(basketProduct, cus);
        }
        public void SaveNewSpecification(ComponentSpecification spec)
        {
            _repo.SaveNewSpecification(spec);
        }
        public void RemoveComponentSpecifications(ComponentSpecification spec)
        {
            _repo.RemoveSpec(spec);
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
        public void SaveManufacturer(Models.Vendors_Producers.Brand newMan)
        {
            _validation.Validate("huh");
            _repo.SaveManufacturer(newMan);
        }
        public List<Models.Vendors_Producers.ChipsetVendor> GetVendors()
        {
            return _repo.GetVendors();
        }
        public List<Models.Vendors_Producers.Brand> GetManufacturers()
        {
            return _repo.GetManufacturers();
        }
        public List<Models.ComponentSpecifications.CPUSocket> GetCPUSockets()
        {
            return _repo.GetSockets();
        }
        public List<Models.ComponentSpecifications.EnergyClass> GetEnergyClasses()
        {
            return _repo.GetEnergyClasses();
        }
        public List<Models.ComponentSpecifications.CPUArchitecture> GetCPUArchitectures()
        {
            return _repo.GetCPUArchitectures();
        }
        public List<Models.ComponentSpecifications.RamProfileFeatures> GetRamProfileFeatures()
        {
            return _repo.GetRamProfileFeatures();
        }

        public List<Models.ComputerComponents.GPU> GetGPUs()
        {
            return _repo.GetGPUs();
        }
        public List<Models.ComputerComponents.CPU> GetCPUs()
        {
            return _repo.GetCPUs();
        }
        public List<Models.ComponentSpecifications.MemoryType> GetMemoryTypes() 
        {
            return _repo.GetMemoryTypes();
        }
    }
}
