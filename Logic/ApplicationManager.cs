using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Logic
{
    //Application manager ser till att vi har en databas igång
    //Den skickar sedan vidare förfrågningar till service som kan validera och hämta info från databasen
    //Vi hanterar även sidor här
    public class ApplicationManager : IDisposable
    {
        private readonly ComponentService _services;
        public ComputerDBContext ComputerPartShopDB { get; } //
        public IPage CurrentPage { get; set; }

        public ApplicationManager(ComponentService service)
        {
            //Instansiera db kontextet här EN gång
            ComputerPartShopDB = new ComputerDBContext();

            //Startsidan blir där man kan browse:a produkter
            CurrentPage = new HomePage();
            _services = service;
        }
        public void SaveChangesOnComponent()
        {
            _services.SaveChangesOnComponent();
        }
        public IEnumerable<ComputerPart> GetComputerComponentsByType(ComputerPart type)
        {
            return _services.GetObjectsOfTheSameType(type);
        }
        public List<Models.Vendors_Producers.Vendor> GetVendors()
        {
            return _services.GetVendors();
        }
        public List<Models.Vendors_Producers.Manufacturer> GetManufacturers()
        {
            return _services.GetManufacturers();
        }
        public List<Models.ComponentSpecifications.CPUSocket> GetCPUSockets()
        {
            return _services.GetCPUSockets();
        }
        public List<Models.ComponentSpecifications.CPUArchitecture> GetCPUArchitectures()
        {
            return _services.GetCPUArchitectures();
        }
        public List<Models.ComponentSpecifications.MemoryType> GetMemoryTypes()
        {
            return _services.GetMemoryTypes();
        }
        public List<Models.ComputerComponents.GPU> GetGPUs()
        {
            return _services.GetGPUs();
        }
        public void SaveNewComponent(ComputerPart part)
        {
            _services.SaveNew(part);
        }
        public void RemoveComponent(ComputerPart part)
        {
            _services.RemoveComponent(part);
        }
        public void SaveCPU(CPU cPU)
        {
            _services.SaveCPU(cPU);
        }
        public void SaveGPU(GPU gPU)
        {
            _services.SaveGPU(gPU);
        }
        public void Dispose() { }   
    }
}
