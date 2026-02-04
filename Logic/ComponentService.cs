using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComputerStoreApplication.Account;
using ComputerStoreApplication.Models.Account;
using ComputerStoreApplication.Crud_Related;
using ComputerStoreApplication.Models.Vendors_Producers;

namespace ComputerStoreApplication.Logic
{
    public class ComponentService
    {
        //Här är steget innan det läggs till i databasen
        //Validera så att sakerna vi lägger till inte redan finns eller är konstigt strukturerat
        //Vi hämtar även information direkt från databasen som vendors, gpus annat
        private readonly ComponentRepo _repo;

        public ComponentService(ComponentRepo repo)
        {
            _repo = repo;
        }
        //public IEnumerable<ComponentSpecification> GetSpecsOTheSameType(ComponentSpecification spec)
        //{
        //    return spec switch
        //    {
        //        CPUArchitecture => _repo.GetCPUArchitectures(),
        //        CPUSocket => _repo.GetSockets(),
        //        EnergyClass => _repo.GetEnergyClasses(),
        //        MemoryType => _repo.GetMemoryTypes(),
        //        RamProfileFeatures => _repo.GetRamProfileFeatures(),

        //        _ => throw new ArgumentException("Unknown specifcation category type")
        //    };
        //}
        public void EditCustomerProfile()
        {
            var customers = _repo.GetCustomersQuired();
            Console.WriteLine("Which customer? Choose customer by inputting Id");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id} {customer.FirstName} {customer.SurName} {customer.PhoneNumber} {customer.Email}");
            }
            int choice = GeneralHelpers.StringToInt();
            var validCustomer = customers.FirstOrDefault(x => x.Id == choice);
            if (validCustomer == null)
            {
                Console.WriteLine("Invalid choice, returning");
                return;
            }
            else
            {
                CustomerHelper.EditCustomerAccount(validCustomer);
                _repo.TrySaveChanges();
            }
        }
        public void AddCountry(Country country)
        {
            using var context = new ComputerDBContext();
            context.Add(country);
        }
        public List<AdminAccount> GetAdmins()
        {
            using var context = new ComputerDBContext();
            return context.Admins.ToList();
        }
        public List<CustomerAccount> GetCustomers()
        {
            using var context = new ComputerDBContext();
            return context.Customers.ToList();
        }
        public List<Order> DisplayCurrentCustomerOrders(int customerId)
        {
            using var context = new ComputerDBContext();
            //alla ordrar
            return context.Orders
              //från kund
              .Where(s => s.CustomerId == customerId)
              //inkludera alla relations till items
              .Include(o => o.OrderItems)
              //order items till products
              .ThenInclude(oi => oi.ComputerPart)
              //även deliveryprovider
              .Include(o => o.DeliveryProvider)
              .ToList();
        }
        public List<Order> GetCustomerOrders(int customerId)
        {
            using var context = new ComputerDBContext();
            return context.Orders.Where(s => s.CustomerId == customerId).ToList();
        }
        public void CreateAccount(string email)
        {
            using var context = new ComputerDBContext();
            bool validFormat = CustomerHelper.CheckIfEmailFormat(email);
            if (!validFormat)
            {
                Console.WriteLine("Invalid format for mail address (Press any key to continue)");
                Console.ReadKey();
                return;
            }
            bool alreadyInUse = context.Customers.Any(o => o.Email.ToLower() == email.ToLower());
            if (alreadyInUse)
            {
                Console.WriteLine("That email is currently already in use, use another if possible");
                return;
            }
            CustomerAccount newCustomer = CustomerAccount.CreateCustomerManual(email);
            newCustomer.CreatePassword();
            Console.WriteLine("You'll find your password in the mail (db)");
            SaveNewCustomer(newCustomer);
            _ = MongoConnection.CustomerRegistration(email, newCustomer.Id);
        }
        public int LoginAdmin(string username, string password)
        {
            using var context = new ComputerDBContext();
            var admin = context.Admins.FirstOrDefault(x => x.UserName == username);
            if (admin == null)
            {
                Console.WriteLine("Couldn't find account");
                return 0;
            }
            if (password == admin.Password)
            {
                Console.WriteLine("Logging in, welcome " + admin.UserName);
                return admin.Id;
            }
            else
            {
                Console.WriteLine("Wrong password, quitting operation");
                return 0;
            }
        }
        public int LogoutAdmin()
        {
            return 0;
        }
        public List<ComputerPart> GetFrontPageProducts()
        {

            return _repo.GetFrontPageProducts();
        }
        public AdminAccount GetAdminInfo(int id)
        {
            var validAdmin = _repo.GetAdmins().FirstOrDefault(x => x.Id == id);
            if (validAdmin != null)
            {
                Console.WriteLine("Found a admin account");
                return validAdmin;
            }
            else
            {
                Console.WriteLine("No admin account found");
                return null;
            }
        }
        public CustomerAccount GetCustomerInfo(int id)
        {
            using var context = new ComputerDBContext();
            var validCustomer = context.Customers.FirstOrDefault(x => x.Id == id);
            if (validCustomer != null)
            {
                return validCustomer;
            }
            else
            {
                return null;
            }

        }
        public int LoginCustomer(string email, string password)
        {
            using var context = new ComputerDBContext();
            //Hitta customer med mail först
            if (string.IsNullOrEmpty(email))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(password))
            {
                return 0;
            }
            var customers = context.Customers;
            var thisCustomer = customers.FirstOrDefault(x => x.Email == email);
            if (thisCustomer==null)
            {
                Console.WriteLine("Coldn't find a account with that mail");
                Console.ReadLine();
                return 0;
            }

            if (password != thisCustomer.Password)
            {
                //Fel lösen
                Console.WriteLine("Wrong password");
                Console.ReadLine();
                return 0;
            }
            else
            {
                Console.WriteLine("Success!");
                Console.ReadLine();
                return thisCustomer.Id;

            }

        }
        public int LogoutCustomer()
        {
            var logout = AccountLogic.LogoutCustomer();
            return logout;
        }
        public List<ComputerPart> GetStoreProducts()
        {
            using var context = new ComputerDBContext();
            return context.CompuerProducts.Include(c=>c.ComponentCategory).Include(x=>x.BrandManufacturer).ToList();
        }
        public List<BasketProduct> GetCustomerItems(int id)
        {
            using var context = new ComputerDBContext();
            return context.BasketProducts.Where(x=>x.CustomerId==id).ToList();
        }
     
        public List<DeliveryProvider> GetDeliveryServices()
        {
            using var context = new ComputerDBContext();


            return context.DeliveryProviders.ToList() ;
        }
        public List<PaymentMethod> GetPaymentMethods()
        {
            using var context = new ComputerDBContext();

            return context.PaymentMethods.ToList();
        }
        public PaymentMethod ChoosePayMethod()
        {
            using var context = new ComputerDBContext();
            var payMethods = context.PaymentMethods;
            Console.WriteLine("Which payment service provider do you want to choose? Input their corresponding Id");
            foreach (var payService in payMethods)
            {
                Console.WriteLine($"Id: {payService.Id} {payService.Name}");
            }
            int choice = GeneralHelpers.StringToInt();
            var valid = payMethods.FirstOrDefault(x => x.Id == choice);
            if (valid != null)
            {
                return valid;
            }
            return null;
        }
        public DeliveryProvider ChooseDeliveryProvider(List<DeliveryProvider> deliveryProviders)
        {
            Console.WriteLine("Which delivery provider do you want to choose? Input their corresponding Id");
            foreach (var deliveryProvider in deliveryProviders)
            {
                Console.WriteLine($"Id: {deliveryProvider.Id} {deliveryProvider.Name}, cost (€): {deliveryProvider.Price}");
            }
            int choice = GeneralHelpers.StringToInt();
            var valid = deliveryProviders.FirstOrDefault(x => x.Id == choice);
            if (valid != null)
            {
                return valid;
            }
            return null;
        }
        public void HandleCustomerPurchase(int customerId)
        {
            //Customer har referens till sina basket items här från customerId
            using var context = new ComputerDBContext();
            var customer = context.Customers.
                Include(c => c.ProductsInBasket).
                ThenInclude(k => k.ComputerPart).
                Include(q => q.CustomerShippingInfos).
                FirstOrDefault(k => k.Id == customerId);
           
            if (customer == null)
            {
                throw new Exception("No customer logged in our found!");
            }
            if (!customer.ProductsInBasket.Any())
            {
                throw new ArgumentOutOfRangeException("Empty basket!");
            }
            if (customer.CustomerShippingInfos == null||customer.CustomerShippingInfos.Count==0) 
            {
                throw new Exception("You need to register an adress to your account"); 
            }
            LocationHolder locationHolder = new LocationHolder
            {
                Cities = _repo.GetCities(),
                Countries = _repo.GetCountries()

            };
            var selectedAddress = CustomerHelper.ChooseAdress(customer.CustomerShippingInfos);
            if (selectedAddress == null)
            {
                throw new Exception("Error, no address found");
            }
            CustomerHelper.MakeSureOfShippingInfoLocation(selectedAddress, locationHolder);
            var basketProducts = customer.ProductsInBasket.Select(k => k.Id).ToList();

            var allProds = GetStoreProducts().Where(p => basketProducts.Contains(p.Id)).ToList();

            var orders = CustomerHelper.HandlePurchase(customer);

            var paymentMethods = GetPaymentMethods();

            var delveryServices = GetDeliveryServices();

            if (orders != null)
            {
                var paymentMethod = ChoosePayMethod();
                orders.ShippingInfoId = selectedAddress.Id;
                orders.ShippingInfo = selectedAddress;
                var deliveryMethod = ChooseDeliveryProvider(delveryServices);
                orders.ApplyPayMethod(paymentMethod);
                orders.ApplyDelivertyMethodAndProvider(deliveryMethod);
                orders.CalculateTotalPrice();
                Console.WriteLine("Go ahead with purchase?");
                bool buy = GeneralHelpers.YesOrNoReturnBoolean();
                //dont wanna buy, return
                if (!buy)
                {
                    return;
                }
                try
                {
                    customer.Orders.Add(orders);
                    customer.ProductsInBasket.Clear();
                   
                    context.SaveChanges();
                    _ = MongoConnection.CustomerPurchase(customerId, orders.TotalCost);
                    Console.WriteLine("Purchase succeded!");
                    Console.ReadLine();

                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Purchase failed: {ex.Message}");
                    Console.ReadLine();
                }
               

            }

        }
        public List<City> GetCities()
        {
            return _repo.GetCities();
        }
        public List<Country> GetCountries()
        {
            return _repo.GetCountries();
        }

        //private void AddNewAddress(CustomerAccount customer, LocationHolder holder)
        //{
        //    //// Choose a countr
        //    //Country chosenCountry = holder.Countries.Any() //if thre's any
        //    //    ? CustomerHelper.ChooseCountryQuestion(holder.Countries.First(), holder) //choose one
        //    //    : GeneralHelpers.ChooseOrCreateCountry(holder.Countries); //else create one

        //    ////if its new
        //    ////new one have a id of 0
        //    //if (chosenCountry.Id == 0)
        //    //{
        //    //    _repo.AddCountry(chosenCountry);
        //    //    _repo.TrySaveChanges();
        //    //    chosenCountry = _repo.GetCountries().First(c => c.Name == chosenCountry.Name);
        //    //}

        //    //// Choose city, like country, by a city is in  acountry
        //    //City chosenCity = holder.Cities.Any(c => c.CountryId == chosenCountry.Id) //same country
        //    //    ? CustomerHelper.ChooseCityQuestion(holder.Cities.First(c => c.CountryId == chosenCountry.Id), holder) //choose one of abailable cities
        //    //    : GeneralHelpers.ChooseOrCreateCity(holder.Cities, chosenCountry); //or create a new one
        //    ////new city
        //    ////add it
        //    //if (chosenCity.Id == 0)
        //    //{
        //    //    chosenCity.Country = chosenCountry;
        //    //    chosenCity.CountryId = chosenCountry.Id;
        //    //    _repo.AddCity(chosenCity);
        //    //    _repo.TrySaveChanges();
        //    //    holder.Cities.Add(chosenCity);
        //    //}

        //    ////we've created at least one city and country
        //    //// Add address
        //    //var newAddress = CustomerHelper.NewAdressQuestionnaire(chosenCity, holder);
        //    //customer.CustomerShippingInfos.Add(newAddress);
        //    //_repo.TrySaveChanges();
        //    //Console.WriteLine("New address added!");
        //}
        public List<BasketProduct> HandleCustomerBasket(int customerId)
        {
            var context = new ComputerDBContext();
            var thisCustomer = context.Customers.Include(q => q.ProductsInBasket)
                .ThenInclude(bp => bp.ComputerPart)
                .FirstOrDefault(x => x.Id == customerId);

            if (thisCustomer == null) 
            { 
                return new List<BasketProduct>(); 
            }

            BasketProduct bask = null;
            if (thisCustomer.ProductsInBasket.Count == 1)
            {
                bask = thisCustomer.ProductsInBasket.First();
            }
            else
            {
                bask = StoreHelper.ChooseWhichBasketItem(thisCustomer.ProductsInBasket);
            }
            if (bask != null)
            {
                //StoreHelper.AdjustQuantityOfBasketItems(bask);
            }
            if (!_repo.TrySaveChanges())
            {
                throw new ArgumentException("Error saving changes");
            }
           
            // Return the same in-memory list
            return thisCustomer.ProductsInBasket.ToList();
        }
        public void SaveNewCustomer(CustomerAccount cus)
        {
            _repo.SaveNewCustomer(cus);
        }
        public void AddProductToBasket(ComputerPart storeProduct, int customerId)
        {
            //validate input before trying to add
            Console.WriteLine("How many do you wish to add to your basket?");
            Console.WriteLine("Max quantity is " + storeProduct.Stock);
            int count = GeneralHelpers.StringToInt();
            if (count > 0 && count <= storeProduct.Stock)
            {
                //Console.WriteLine("Valid");
            }
            else if(count> storeProduct.Stock)
            {
                Console.WriteLine("Can't order that many!");
                Console.ReadLine();
                return;
            }
            else if (count == 0||count<0)
            {
                Console.WriteLine("Can't order 0 or fewer!");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("Coudn't read input, returning...!");
                Console.ReadLine();
                return;
            }
            int prodId = storeProduct.Id;

            _repo.AddProductToBasket(prodId, count, customerId);
        }
        public void SaveManufacturer(Models.Vendors_Producers.Brand newMan)
        {
            _repo.SaveManufacturer(newMan);
        }
        public List<Models.Vendors_Producers.Brand> GetManufacturers()
        {
            return _repo.GetManufacturers();
        }
       
        internal ComponentCategory GetCategory(int productId)
        {
           return _repo.GetCatagory(productId);
        }

        internal Brand GetBrand(int productId)
        {
            return _repo.GetBrand(productId);
        }
    }
}
