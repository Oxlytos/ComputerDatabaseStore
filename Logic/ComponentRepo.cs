using ComputerStoreApplication.Account;
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
        private readonly ComputerDBContext _dbContext;
        public ComponentRepo(ComputerDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<CustomerShippingInfo> GetAdressesOfCustomer(int customerId)
        {
            using var context = new ComputerDBContext();
            return context.CustomerShippingInfos.Where(x => x.CustomerId == customerId).ToList();
        }
        //}
        public List<Brand> GetManufacturers()
        {
            using var context = new ComputerDBContext();
            return context.BrandManufacturers.Cast<Brand>().ToList();
        }
        public List<BasketProduct> GetCustomerItems(int id)
        {
            using var context = new ComputerDBContext();
            var theseObject = context.BasketProducts.Where(x => x.CustomerId == id).Include(s=>s.ComputerPart);
            return theseObject.ToList();
        }
        public void AddCountry(Country country)
        {
            using var context = new ComputerDBContext();
            context.Countries.Add(country);
        }
        public void AddCity(City city)
        {
            using var context = new ComputerDBContext();
            context.Cities.Add(city);
        }
        public List<City> GetCities()
        {
            using var context = new ComputerDBContext();
            return context.Cities.ToList();
        }
        public List<Country> GetCountries()
        {
            using var context = new ComputerDBContext();
            return context.Countries.ToList();
        }
        public List<BasketProduct> GetCustomerItemsForBasket(int customerId)
        {
            using var context = new ComputerDBContext();
            return context.BasketProducts
                .Include(bp => bp.ComputerPart)      // Include the Product navigation property
                .Where(bp => bp.CustomerId == customerId)
                .ToList();                      // Return as a List<BasketProduct>
        }
        public List<ComputerPart> GetStoreProducts()
        {
            using var context = new ComputerDBContext();
            return context.CompuerProducts.Include(z=>z.BrandManufacturer).Include(k=>k.ComponentCategory).ToList();
        }

        public List<CustomerAccount> GetCustomers()
        {
            using var context = new ComputerDBContext();
            return context.Customers.ToList();
        }
        public List<Order> GetOrders()
        {
            using var context = new ComputerDBContext();
            return context.Orders.ToList();
        }
        public List<ComputerPart> GetFrontPageProducts()
        {
            using var context = new ComputerDBContext();
            var returnList = context.CompuerProducts
             .Where(s => s.SelectedProduct && s.Stock > 0).Include(s=>s.ComponentCategory).Include(b=>b.BrandManufacturer)
               .ToList();
                return returnList;
           
        }
        public List<Order> GetOrdersQuired()
        {
            using var context = new ComputerDBContext();
            return context.Orders.ToList();
        }
        public List<CustomerAccount> GetCustomersQuired()
        {
            using var context = new ComputerDBContext();
            return context.Customers.ToList();
        }
        public List<DeliveryProvider> GetDeliveryServices()
        {
            using var context = new ComputerDBContext();
            return context.DeliveryProviders.ToList();
        }
        public List<PaymentMethod> GetPayrmentMethods()
        {
            using var context = new ComputerDBContext();
            return context.PaymentMethods.ToList();
        }
        public List<AdminAccount> GetAdmins()
        {
            using var context = new ComputerDBContext();
            return context.Admins.ToList();
        }
        public void AddProductToBasket(int prod, int count, int customerId)
        {
            using var context = new ComputerDBContext();
            if (customerId == 0)
            {
                throw new InvalidOperationException("User must be logged in to add products to basket");
            }
            //work with trackedcustomer thats part of ef
            var trackedCustomerInfo = context.Customers.Include(q => q.ProductsInBasket).FirstOrDefault(c => c.Id == customerId);
            if (trackedCustomerInfo == null)
            {

                throw new InvalidOperationException("Customer not found");
            }
            var basketItem = trackedCustomerInfo.ProductsInBasket.FirstOrDefault(bp => bp.CustomerId == customerId && bp.ComputerPartId == prod);
            //check existence
            if (basketItem != null)
            {
                basketItem.Quantity += count;
            }
            else
            {
                var newItem = new BasketProduct
                {
                    CustomerId = trackedCustomerInfo.Id,
                    ComputerPartId = prod,
                    Quantity = count
                };
                trackedCustomerInfo.ProductsInBasket.Add(newItem); // <-- add directly to DbSet othwerise EF cries a bit
               
            }

            //Lower store stock of x item
            var storeObjects = context.CompuerProducts.FirstOrDefault(x=>x.Id==prod);
            if (storeObjects != null)
            {
                storeObjects.Stock -= count;
            }
            //when troubleshooting tracking
            foreach (var bp in context.ChangeTracker.Entries<BasketProduct>())
            {
                Console.WriteLine($"Tracked BasketProduct Id: {bp.Entity.Id}, CustomerId: {bp.Entity.CustomerId}");
            }
            context.SaveChanges();
            //TrySaveChanges();
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
            using var context = new ComputerDBContext();
            if (!context.Customers.Contains(customer))
            {
                context.Add(customer);
                TrySaveChanges();
            }
            else
            {
                Console.WriteLine("Customer already exists");
            }
        }
        public bool TrySaveChanges()
        {
            //foreach (var bp in _dbContext.ChangeTracker.Entries<BasketProduct>())
            //{
            //    Console.WriteLine($"Tracked BasketProduct Id: {bp.Entity.Id}, CustomerId: {bp.Entity.CustomerId}");
            //}
            //Console.ReadLine();
            try
            {
                Console.WriteLine("DB Operation succesfull, press enter to continue");
                Console.ReadLine();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrency error when updating: {ex.Message}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error when trying to save: {ex.Message}");
                return false;
            }
            catch (DbException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }
        public void SaveManufacturer(Brand manufacturer)
        {
            using var context = new ComputerDBContext();
            context.BrandManufacturers.Add(manufacturer);
            Console.WriteLine("Saved manufacturer!");
            Console.ReadLine();
            TrySaveChanges();

        }

        internal ComponentCategory GetCatagory(int productId)
        {
            using var context = new ComputerDBContext();
            return context.ComponentCategories.FirstOrDefault(x => x.Id == productId);
        }

        internal Brand GetBrand(int productId)
        {
            using var context = new ComputerDBContext();
            return context.BrandManufacturers.FirstOrDefault(x => x.Id == productId);
        }
    }
}
