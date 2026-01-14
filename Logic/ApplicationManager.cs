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
            CurrentPage = new BrowseProducts();
            _services = service;
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
        public void SaveCPU(CPU cPU)
        {
            _services.SaveCPU(cPU);
        }
        public void Dispose() { }   
    }
}
