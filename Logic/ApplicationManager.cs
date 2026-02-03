using ComputerStoreApplication.Account;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using ComputerStoreApplication.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ComputerStoreApplication.Logic
{
    //Application manager ser till att vi har en databas igång
    //Den skickar sedan vidare förfrågningar till service som kan validera och hämta info från databasen
    //Vi hanterar även sidor här
    public class ApplicationManager : IDisposable
    {
        private readonly ComponentService _services;

        public  MongoConnection _mongoConnection;
        public DapperService Dapper {  get; private set; }
        public ComputerDBContext ComputerPartShopDB { get; } //
        public IPage CurrentPage { get; set; }
        public int CustomerId { get; set; }
        public bool IsLoggedInAsCustomer => CustomerId != 0;

        public int AdminId { get; set; }
        public bool IsLoggedInAsAdmin => AdminId != 0;
        public List<ComputerPart> ProductsInBasket { get; set; }

        public ApplicationManager(ComponentService service, MongoConnection mongo)
        {
            //Instansiera db kontextet här EN gång
            ComputerPartShopDB = new ComputerDBContext();
            Dapper = new DapperService(ComputerPartShopDB);

            //Startsidan blir där man kan browse:a produkter
            CurrentPage = new HomePage();
            ProductsInBasket = new List<ComputerPart>();
            _services = service;
            _mongoConnection = mongo;
            CustomerId = 0;
            AdminId = 0;
        }
        public ComponentCategory GetCategory(int productId)
        {
           return _services.GetCategory(productId);
        }
        public Brand GetBrand(int productId)
        {
            return _services.GetBrand( productId);
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
                _ = MongoConnection.AdminLoginAttempt(username, adminId, true);
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
        public List<ComputerPart> GetFrontPageProducts()
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
            Console.WriteLine("Login attempt started");
            var thisCustomerId = _services.LoginCustomer(email, password);
            if (thisCustomerId != 0)
            {
                CustomerId = thisCustomerId;
                _ = MongoConnection.CustomerLoginAttempt(email, thisCustomerId, true);
                return true;
            }
            _ = MongoConnection.CustomerLoginAttempt(email, 0, false);
            return false;

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
        public void AddStoreProductToBasket(int customerId, ComputerPart prod)
        {
            _services.AddProductToBasket(prod, customerId);
        }
        public void SaveChangesOnComponent()
        {
            _services.SaveChangesOnComponent();
        }
        public List<ComputerPart> GetStoreProducts()
        {
            return _services.GetStoreProducts();
        }
        public List<BasketProduct> GetBasketProductsFromCustomerId(int customerId)
        {
            return _services.GetCustomerItems(customerId);
        }
        //public IEnumerable<ComputerPart> GetComputerComponentsByType(ComputerPart type)
        //{
        //    return _services.GetObjectsOfTheSameType(type);
        //}
        //public List<Models.Vendors_Producers.ChipsetVendor> GetVendors()
        //{
        //    return _services.GetVendors();
        //}
        public List<ComputerPart> GetComputerParts()
        {
            return ComputerPartShopDB.CompuerProducts.ToList();
        }
        public List<ComponentCategory> GetCategories()
        {
            return ComputerPartShopDB.ComponentCategories.ToList();
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
        /*
        public List<Models.ComputerComponents.GPU> GetGPUs()
        {
            return _services.GetGPUs();
        }
        public List<Models.ComputerComponents.CPU> GetCPUs()
        {
            return _services.GetCPUs();
        }*/
        public void AddProductToBasket(ComputerPart prod, int customerId)
        {
            _services.AddProductToBasket(prod, customerId);
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
        public void SaveNewStoreProduct(ComputerPart prodc)
        {
            _services.SaveNew(prodc);
        }
        public void RemoveComponent(ComputerPart part)
        {
            _services.RemoveComponent(part);
        }
        public void RefreshCurrentCustomerBasket(CustomerAccount customer)
        {
            if (customer == null) 
            { 
                return; 
            }
            customer.ProductsInBasket = ComputerPartShopDB.BasketProducts
          .Where(b => b.CustomerId == customer.Id && b.Quantity > 0)
          .Include(b => b.ComputerPart)
          .ToList();
        }
        public void RemoveComponentSpecifications(ComponentSpecification speec)
        {
            _services.RemoveComponentSpecifications(speec);
        }
        /*
        public void SaveCPU(CPU cPU)
        {
            _services.SaveCPU(cPU);
        }
        public void SaveGPU(GPU gPU)
        {
            _services.SaveGPU(gPU);
        }*/
        public void Dispose() { }
        public void VerifyStoreItems()
        {
            //Make sure user can't buy products no longer in stock
            //we load this when they load the checkout page

            //var itemtsToRemove =ComputerPartShopDB.BasketProducts.Where
            //    (b=>b.Quantity==0||ComputerPartShopDB.CompuerProducts.
            //    Any(p=>p.Id==b.ComputerPartId&&p.Stock==0))
            //    .ToList();

            //ComputerPartShopDB.RemoveRange(itemtsToRemove);
            //ComputerPartShopDB.SaveChanges();

            var outOfStockItems = ComputerPartShopDB.CompuerProducts
               .Where(p => p.Stock == 0)
               .Select(p => p.Id)
               .ToList();

        }
        public void VerifyBasketItems(int? currentCustomerId)
        {
            //var zeroItems = ComputerPartShopDB.BasketProducts .Where(b => b.CustomerId == CustomerId && b.Quantity == 0).ToList();
            var basketItems = ComputerPartShopDB.BasketProducts.Where(b => b.CustomerId == currentCustomerId && b.Quantity > 0) .ToList();
            //ComputerPartShopDB.BasketProducts.RemoveRange(zeroItems);
            //ComputerPartShopDB.SaveChanges();
        }
        public void InformOfQuittingOperation()
        {
            Console.WriteLine("Quitting operation... (Press any key to continue)");
            Console.ReadKey();
        }

        internal void AdjustBasketItems(CustomerAccount? currentCustomer, List<BasketProduct> basketProducts)
        {
            if (basketProducts.Count == 0)
            {
                return;
            }
            else if (basketProducts.Count > 1)
            {
                Console.WriteLine("Which basket item? Input their Id, cancel operation by submitting 0");
                foreach (var product in basketProducts)
                {
                    Console.WriteLine($"Id: {product.Id} {product.ComputerPart.Name} Quantity: {product.Quantity}");
                }
                int choice = GeneralHelpers.ReturnValidIntOrNone();
                if(choice == 0)
                {
                    return;
                }
                var valid = basketProducts.FirstOrDefault(X => X.Id == choice);
                if (valid == null)
                {
                    return ;
                }
                var storeItem = GetStoreProducts().FirstOrDefault(x => x.Id == valid.ComputerPartId);
                Console.WriteLine($"How many of this product? Maximum is {storeItem.Stock}");
                int amount = GeneralHelpers.StringToInt();
                if (amount > storeItem.Stock)
                {
                    Console.WriteLine("Input is greater than available stock, we've capped it at available amount");
                    amount= storeItem.Stock;
                }
            }
            else
            {
                var item = basketProducts.First(); 
                var storeItem = GetStoreProducts().FirstOrDefault(x => x.Id == item.ComputerPartId);
                Console.WriteLine($"How many of this product? Maximum is {storeItem.Stock}");
                int amount = GeneralHelpers.StringToInt();
                if (amount > storeItem.Stock)
                {
                    Console.WriteLine("Input is greater than available stock, we've capped it at available amount");
                    amount = storeItem.Stock;
                }

            }
                return;

        }
    }
}
