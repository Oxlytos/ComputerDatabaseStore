using ComputerStoreApplication.Account;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
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
        public DapperHelper Dapper {  get; private set; }
        public ComputerDBContext ComputerPartShopDB { get; } //
        public IPage CurrentPage { get; set; }
        public int CustomerId { get; set; }
        public bool IsLoggedInAsCustomer => CustomerId != 0;

        public int AdminId { get; set; }
        public bool IsLoggedInAsAdmin => AdminId != 0;
        public List<StoreProduct> ProductsInBasket { get; set; }

        public ApplicationManager(ComponentService service)
        {
            //Instansiera db kontextet här EN gång
            ComputerPartShopDB = new ComputerDBContext();
            Dapper = new DapperHelper(ComputerPartShopDB);

            //Startsidan blir där man kan browse:a produkter
            CurrentPage = new HomePage();
            ProductsInBasket = new List<StoreProduct>();
            _services = service;
            CustomerId = 0;
            AdminId = 0;
        }
        public void EditCustomerProfile()
        {
            _services.EditCustomerProfile();
        }
        public List<CustomerAccount> GetCustomers()
        {
            return _services.GetCustomers();
        }
        public bool LoginAsAdmin(string username, string password)
        {
            if (IsLoggedInAsCustomer) 
            {
                Console.WriteLine("Can't login as a admin while logged in as a customer");
                Console.ReadLine();
                return false;
            }
            Console.WriteLine("Trying to login as admin");
            var adminId = _services.LoginAdmin(username, password);
            if (adminId != 0)
            {
                Console.WriteLine("Success");
                AdminId = adminId;
                Console.ReadLine();
                return true;
            }
            else
            {
                Console.WriteLine("Wrong credentials, returning");
                Console.ReadLine();
                return false;
            }
        }
        public void CreateAccount(string email)
        {
            _services.CreateAccount(email);
        }
        public List<StoreProduct> GetFrontPageProducts()
        {
            return _services.GetFrontPageProducts();
        }
        public List<Order> GetCustomerOrders(int customerId)
        {
            return _services.DisplayCurrentCustomerOrders(customerId);
        }

        public bool LogoutAsAdmin()
        {
            Console.WriteLine("Logged out");
            AdminId = 0;
            return false;
        }
        public AdminAccount GetAdminInfo(int id)
        {
            return _services.GetAdminInfo(id);
        }
        public CustomerAccount GetCustomerInfo(int id)
        {
            return _services.GetCustomerInfo(id);
        }
        public bool LoginAsCustomer(string email, string password)
        {
            if (IsLoggedInAsAdmin)
            {
                Console.WriteLine("Can't login as customer while in admin login");
                Console.ReadLine();
                return false ;
            }
            Console.WriteLine("Tryna login");
            var thisCustomerId = _services.LoginCustomer(email, password);
            if (thisCustomerId != 0)
            {
                CustomerId = thisCustomerId;
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool LogoutAsCustomer()
        {
            Console.WriteLine("Logged out");
            CustomerId = 0;
            return false;
        }
        public void HandleCustomerPurchase(int customerId)
        {
            _services.HandleCustomerPurchase(customerId);
        }
        public void HandleCustomerShippingInfo(int customerId)
        {
            _services.HandleCustomerShippingInfo(customerId);
        }
        public List<BasketProduct> HandleCustomerBasket(int customerId)
        {
            return _services.HandleCustomerBasket(customerId);
        }
        public void AddStoreProductToBasket(CustomerAccount cus, StoreProduct prod)
        {
            _services.AddProductToBasket(prod, cus);
        }
        public void SaveChangesOnComponent()
        {
            _services.SaveChangesOnComponent();
        }
        public List<StoreProduct> GetStoreProducts()
        {
            return _services.GetStoreProducts();
        }
        public List<BasketProduct> GetBasketProductsFromCustomerId(int customerId)
        {
            return _services.GetCustomerItems(customerId);
        }
        public IEnumerable<ComputerPart> GetComputerComponentsByType(ComputerPart type)
        {
            return _services.GetObjectsOfTheSameType(type);
        }
        public IEnumerable<ComponentSpecification> GetComponentSpecifications(ComponentSpecification spec)
        {
            return _services.GetSpecsOTheSameType(spec);
        }
        public List<Models.Vendors_Producers.ChipsetVendor> GetVendors()
        {
            return _services.GetVendors();
        }
        public List<Models.Vendors_Producers.Brand> GetManufacturers()
        {
            return _services.GetManufacturers();
        }
        public List<Models.ComponentSpecifications.EnergyClass> GetEnergyClasses()
        {
            return _services.GetEnergyClasses();
        }
        public List<Models.ComponentSpecifications.RamProfileFeatures> GetRamProfileFeatures()
        {
            return _services.GetRamProfileFeatures();
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
        public List<Models.ComputerComponents.CPU> GetCPUs()
        {
            return _services.GetCPUs();
        }
        public void AddProductToBasket(StoreProduct prod, CustomerAccount customer)
        {
            _services.AddProductToBasket(prod, customer);
        }
        public void SaveNewSpecification(ComponentSpecification spec)
        {
            _services.SaveNewSpecification(spec);
        }
        public void SaveNewCustomer(CustomerAccount cus)
        {
            _services.SaveNewCustomer(cus);
        }
        public void SaveNewComponent(ComputerPart part)
        {
            _services.SaveNew(part);
        }
        public void SaveNewStoreProduct(StoreProduct prodc)
        {
            _services.SaveNew(prodc);
        }
        public void RemoveComponent(ComputerPart part)
        {
            _services.RemoveComponent(part);
        }
        public void RemoveComponentSpecifications(ComponentSpecification speec)
        {
            _services.RemoveComponentSpecifications(speec);
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
