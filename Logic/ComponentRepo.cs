using ComputerStoreApplication.Account;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
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
        /*
        public List<ComputerPart?> GetAllProducts()
        {
            //Cast:a alla object som ComputerPart, concat till ett lång mixad lista med objekt av samma basklass
            return _dbContext.CPUs.Cast<ComputerPart>()
                 .Concat(_dbContext.GPUs)
                 .Concat(_dbContext.RAMs)
                 .Concat(_dbContext.PSUs)
                 .Concat(_dbContext.Motherboards).
                 ToList();
        }*/
        public List<CustomerShippingInfo> GetAdressesOfCustomer(int customerId)
        {
            return _dbContext.CustomerShippingInfos.Where(x => x.CustomerId == customerId).ToList();
        }
        //public List<RAM> GetRAMs()
        //{
        //    return _dbContext.RAMs.Cast<RAM>().ToList();
        //}
        //public List<Motherboard> GetMotherboards()
        //{
        //    return _dbContext.Motherboards.Cast<Motherboard>().ToList();
        //}
        //public List<PSU> GetPSUs()
        //{
        //    return _dbContext.PSUs.Cast<PSU>().ToList();
        //}
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
        public List<BasketProduct> GetCustomerItems(int id)
        {
            var theseObject = _dbContext.BasketProducts.Where(x => x.CustomerId == id);
            return theseObject.ToList();
        }
        public void AddCountry(Country country)
        {
            _dbContext.Countries.Add(country);
        }
        public void AddCity(City city)
        {
            _dbContext.Cities.Add(city);
        }
        public List<City> GetCities()
        {
            return _dbContext.Cities.ToList();
        }
        public List<Country> GetCountries()
        {
            return _dbContext.Countries.ToList();
        }
        public List<BasketProduct> GetCustomerItemsForBasket(int customerId)
        {
            return _dbContext.BasketProducts
                .Include(bp => bp.ComputerPart)      // Include the Product navigation property
                .Where(bp => bp.CustomerId == customerId)
                .ToList();                      // Return as a List<BasketProduct>
        }
        public List<ComputerPart> GetStoreProducts()
        {
            return _dbContext.CompuerProducts.Include(z=>z.BrandManufacturer).Include(k=>k.ComponentCategory).ToList();
        }
        //public List<GPU> GetGPUs()
        //{
        //    return _dbContext.GPUs.ToList();
        //}
        //public List<CPU> GetCPUs()
        //{
        //    var cpus = _dbContext.CPUs.ToList();

        //    //Koppla ihop virtuella proprterties här
        //    var man = GetManufacturers();
        //    var vendors = GetVendors();
        //    var sockets = GetSockets();
        //    var cpuArchs = GetCPUArchitectures();
        //    foreach (var cpu in cpus)
        //    {
        //        cpu.BrandManufacturer = man.FirstOrDefault(s => s.Id == cpu.BrandId);
        //        cpu.ChipsetVendor = vendors.FirstOrDefault(s => s.Id == cpu.ChipsetVendorId);
        //        cpu.SocketType = sockets.FirstOrDefault(s => s.Id == cpu.SocketId);
        //        cpu.CPUArchitecture = cpuArchs.FirstOrDefault(s => s.Id == cpu.CPUArchitectureId);

        //    }

        //    return cpus;
        //}

        public List<CustomerAccount> GetCustomers()
        {
            return _dbContext.Customers.ToList();
        }
        public List<Order> GetOrders()
        {
            return _dbContext.Orders.ToList();
        }
        public List<ComputerPart> GetFrontPageProducts()
        {

            var returnList = _dbContext.CompuerProducts
             .Where(s => s.SelectedProduct && s.Stock > 0)
               .ToList();
            if(returnList.Count > 0)
            {
                return returnList;
            }
            return null;
           
        }
        public IQueryable<Order> GetOrdersQuired()
        {
            return _dbContext.Orders;
        }
        public IQueryable<CustomerAccount> GetCustomersQuired()
        {
            return _dbContext.Customers;
        }
        public List<DeliveryProvider> GetDeliveryServices()
        {
            return _dbContext.DeliveryProviders.ToList();
        }
        public List<PaymentMethod> GetPayrmentMethods()
        {
            return _dbContext.PaymentMethods.ToList();
        }
      
        public void SaveNew(ComputerPart part)
        {
            _dbContext.CompuerProducts.Add(part);
            TrySaveChanges();
        }
        public void SaveNewSpecification(ComponentSpecification spec)
        {
            _dbContext.AllComponentSpecifcations.Add(spec);
            TrySaveChanges();
        }
        public List<AdminAccount> GetAdmins()
        {
            return _dbContext.Admins.ToList();
        }
        public void AddProductToBasket(int prod, int count, CustomerAccount cus)
        {
            if (cus == null)
            {
                Console.WriteLine("You need to be logged in to add to basket");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Checking if already exists");
            Console.ReadLine();

            //kunddata
            var trackedCustomerInfo = _dbContext.Customers.Include(q => q.ProductsInBasket).FirstOrDefault(c => c.Id == cus.Id);

            if (trackedCustomerInfo == null) 
            {
                return;
            }
            var checkIfExistingProductInBasket = trackedCustomerInfo.ProductsInBasket.FirstOrDefault(x => x.ComputerPartId == prod);
            Console.ReadLine();
            if (checkIfExistingProductInBasket!=null)
            {
                Console.WriteLine("You already have this product in your basket");
                Console.WriteLine("Increased quantity by previous input amount");
                count += checkIfExistingProductInBasket.Quantity;
            }
            else
            {
                var newItem = new BasketProduct
                {
                    CustomerId = cus.Id,
                    ComputerPartId = prod,
                    Quantity =count
                };
                Console.WriteLine("Added to basket");
                cus.ProductsInBasket.Add(newItem);
            }

            //Sänk stock av store objekt antal, de håller på att köpas här
            var storeObjects = _dbContext.CompuerProducts.FirstOrDefault(x=>x.Id==prod);
            if (storeObjects != null)
            {
                storeObjects.Stock -= count;
                if(storeObjects.Stock < 0)
                {
                    storeObjects.SelectedProduct = false;
                     storeObjects.Stock = 0;
                }
            }
            TrySaveChanges();
        }
        public void RemoveSingularObjectFromBasket(BasketProduct prod, CustomerAccount cus)
        {
            if (cus.ProductsInBasket.Contains(prod))
            {
                var thisItem = cus.ProductsInBasket.FirstOrDefault(x => x.Id == prod.Id);
                if (thisItem != null)
                {
                    if (thisItem.Quantity == 0 || thisItem.Quantity == 1)
                    {
                        RemoveFromBasket(prod, cus);
                    }
                }
            }
        }
        public void RemoveFromBasket(BasketProduct prod, CustomerAccount cus)
        {
            if (cus.ProductsInBasket.Contains(prod))
            {
                cus.ProductsInBasket.Remove(prod);
            }
            else
            {
                Console.WriteLine("Error when trying to remove basket item");
            }
        }
        public void SaveNewCustomer(CustomerAccount customer)
        {
            if (!_dbContext.Customers.Contains(customer))
            {
                _dbContext.Add(customer);
                TrySaveChanges();
            }
            else
            {
                Console.WriteLine("Customer already exists");
            }
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
        //public void SaveNewCPU(CPU cpu)
        //{
        //    _dbContext.CPUs.Add(cpu);
        //    TrySaveChanges();
        //}
        //public void SaveNewGPU(GPU gpu)
        //{
        //    _dbContext.GPUs.Add(gpu);
        //    TrySaveChanges();
        //}
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
            catch (DbException ex)
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
