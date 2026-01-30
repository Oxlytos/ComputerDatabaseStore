using ComputerStoreApplication.Helpers;
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
using Microsoft.EntityFrameworkCore;
using ComputerStoreApplication.Account;
using ComputerStoreApplication.Models.Account;

namespace ComputerStoreApplication.Logic
{
    public class ComponentService
    {
        //Här är steget innan det läggs till i databasen
        //Validera så att sakerna vi lägger till inte redan finns eller är konstigt strukturerat
        //Vi hämtar även information direkt från databasen som vendors, gpus annat
        private readonly ComponentRepo _repo;
        private readonly ValidationManager _validation;

        public ComponentService(ComponentRepo repo, ValidationManager val)
        {
            _repo = repo;
        }
        //Fältet var static, kan inte vara om det ska connectad till resten
        public IEnumerable<ComputerPart> GetObjectsOfTheSameType(ComputerPart part)
        {
            return part switch
            {
                GPU => _repo.GetGPUs(),
                CPU => _repo.GetCPUs(),
                RAM => _repo.GetRAMs(),
                Motherboard => _repo.GetMotherboards(),
                PSU => _repo.GetPSUs(),


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
        public void EditCustomerProfile()
        {
            var customers = _repo.GetCustomersQuired();
            Console.WriteLine("Which customer? Choose customer by inputting Id");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id} {customer.FirstName} {customer.SurName} {customer.PhoneNumber} {customer.Email}");
            }
            int choice = GeneralHelpers.StringToInt(Console.ReadLine());
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
        }
        public List<AdminAccount> GetAdmins()
        {
            return _repo.GetAdmins();
        }
        public List<CustomerAccount> GetCustomers()
        {
            return _repo.GetCustomers();
        }
        //Stort return objekt för alla display värden
        //Kan göra ett annat objekt som bara har relevanta fält
        //Fast det är lite knappert med tid
        public List<Order> DisplayCurrentCustomerOrders(int customerId)
        {
            //alla ordrar
            return _repo.GetOrdersQuired()
              //från kund
              .Where(s => s.CustomerId == customerId)
              //inkludera alla relations till items
              .Include(o => o.OrderItems)
              //order items till products
              .ThenInclude(oi => oi.Product)
              //även deliveryprovider
              .Include(o => o.DeliveryProvider)
              .ToList();
        }
        public List<Order> GetCustomerOrders(int customerId)
        {
            return _repo.GetOrders().Where(s => s.CustomerId == customerId).ToList();
        }
        public void CreateAccount(string email)
        {
            bool validFormat = CustomerHelper.CheckIfEmailFormat(email);
            if (!validFormat)
            {
                return;
            }
            bool alreadyInUse = _repo.GetCustomers().Any(o => o.Email == email);
            if (alreadyInUse)
            {
                Console.WriteLine("That email is currently already in use, use another if possible");
                return;
            }
            CustomerAccount newCustomer = CustomerAccount.CreateCustomerManual(email);
            newCustomer.CreatePassword();
            SaveNewCustomer(newCustomer);
            SaveChangesOnComponent();
            Console.WriteLine("You'll find your password in the mail (db)");
            Console.WriteLine("Now, input the password");
            string password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                return;
            }

            HandleCustomerShippingInfo(newCustomer.Id);

        }
        public int LoginAdmin(string username, string password)
        {
            var admin = _repo.GetAdmins().FirstOrDefault(x => x.UserName == username);
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
        public List<StoreProduct> GetFrontPageProducts()
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
            var validCustomer = _repo.GetCustomers().FirstOrDefault(x => x.Id == id);
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
            //Hitta customer med mail först
            if (string.IsNullOrEmpty(email))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(password))
            {
                return 0;
            }
            var customers = _repo.GetCustomers();
            var thisCustomer = customers.FirstOrDefault(x => x.Email == email);

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
        public List<StoreProduct> GetStoreProducts()
        {
            return _repo.GetStoreProducts();
        }
        public List<BasketProduct> GetCustomerItems(int id)
        {
            return _repo.GetCustomerItems(id);
        }
        public void SaveNew(StoreProduct storeProduct)
        {
            _repo.SaveNew(storeProduct);
        }
        public void SaveNew(ComputerPart part)
        {
            _repo.SaveNew(part);
        }
        public List<DeliveryProvider> GetDeliveryServices()
        {
            return _repo.GetDeliveryServices();
        }
        public List<PaymentMethod> GetPaymentMethods()
        {
            return _repo.GetPayrmentMethods();
        }
        public PaymentMethod ChoosePayMethod(List<PaymentMethod> paymentMethods)
        {
            Console.WriteLine("Which payment service provider do you want to choose? Input their corresponding Id");
            foreach (var payService in paymentMethods)
            {
                Console.WriteLine($"Id: {payService.Id} {payService.Name}");
            }
            int choice = GeneralHelpers.StringToInt(Console.ReadLine());
            var valid = paymentMethods.FirstOrDefault(x => x.Id == choice);
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
            int choice = GeneralHelpers.StringToInt(Console.ReadLine());
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
            var customer = _repo.GetCustomersQuired().
                Include(c => c.ProductsInBasket).
                ThenInclude(k => k.Product).
                Include(q => q.CustomerShippingInfos).
                FirstOrDefault(k => k.Id == customerId);
            if (customer == null)
            {
                return;
            }
            if (customer.CustomerShippingInfos == null) 
            {
                Console.WriteLine("You need to register an adress to your account");
                Console.ReadLine();
                return;
            }

            LocationHolder locationHolder = new LocationHolder
            {
                Cities = _repo.GetCities(),
                Countries = _repo.GetCountries()

            };
            var selectedAddress = CustomerHelper.ChooseAdress(customer.CustomerShippingInfos);
            CustomerHelper.MakeSureOfShippingInfoLocation(selectedAddress, locationHolder);
            var prodIds = customer.ProductsInBasket.Select(k => k.Id).ToList();

            var allProds = GetStoreProducts().Where(p => prodIds.Contains(p.Id)).ToList();

            var orders = CustomerHelper.HandlePurchase(customer);

            var paymentMethods = GetPaymentMethods();

            var delveryServices = GetDeliveryServices();

            if (orders != null)
            {
                var paymentMethod = ChoosePayMethod(paymentMethods);
                orders.ShippingInfoId = selectedAddress.Id;
                orders.ShippingInfo = selectedAddress;
                var deliveryMethod = ChooseDeliveryProvider(delveryServices);
                orders.ApplyPayMethod(paymentMethod);
                orders.ApplyDelivertyMethodAndProvider(deliveryMethod);
                orders.CalculateTotalPrice();
                Console.WriteLine("Go ahead with purchase?");
                bool buy = GeneralHelpers.YesOrNoReturnBoolean();
                if (buy)
                {
                    customer.ProductsInBasket.Clear();
                    customer.Orders.Add(orders);
                    bool sucess = _repo.TrySaveChanges();
                }
                else
                {
                    return;
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

        public void HandleCustomerShippingInfo(int customerId)
        {
            var customer = _repo.GetCustomersQuired()
                .Include(c => c.CustomerShippingInfos)
                    .ThenInclude(s => s.City)
                    .ThenInclude(c => c.Country)
                .FirstOrDefault(x => x.Id == customerId);

            if (customer == null)
            {
                return; // No customer, just return, how'd they get here
            }
            if (customer.CustomerShippingInfos != null||customer.CustomerShippingInfos.Count==0)
            {
                Console.WriteLine("Found these addresses");
                foreach (var address in customer.CustomerShippingInfos)
                {
                    Console.WriteLine($"{address.StreetName} {address.PostalCode} {address.City.Name} {address.City.Country.Name}");
                }
            }
            Console.WriteLine("Do you want to [C]reate a new address, [U]pdate an existing one, [D]elete one, or [S]kip?");
            var choiceKey = Console.ReadKey(true).Key;
            LocationHolder locationHolder = new LocationHolder
            {
                Countries = _repo.GetCountries(),
                Cities = _repo.GetCities()
            };
            switch (choiceKey)
            {
                case ConsoleKey.C:
                    Console.WriteLine("Let's add a new address.");
                    AddNewAddress(customer, locationHolder);
                    break;
                case ConsoleKey.U: // Update
                    var toUpdate = CustomerHelper.ChooseAdress(customer.CustomerShippingInfos);
                    if (toUpdate != null)
                    {
                        CustomerHelper.AdressQuestionnaire(toUpdate, toUpdate.City, locationHolder);
                        _repo.TrySaveChanges();
                        Console.WriteLine("Address updated!");
                    }
                    break;
                case ConsoleKey.D: // Delete
                    var toDelete = CustomerHelper.ChooseAdress(customer.CustomerShippingInfos);
                    if (toDelete != null)
                    {
                        customer.CustomerShippingInfos.Remove(toDelete);
                        _repo.TrySaveChanges();
                        Console.WriteLine("Address deleted!");
                    }
                    break;
                case ConsoleKey.S: // Skip
                    Console.WriteLine("Skipping address changes.");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Skipping.");
                    break;
            }
        }
        private void AddNewAddress(CustomerAccount customer, LocationHolder holder)
        {
            // Choose a countr
            Country chosenCountry = holder.Countries.Any() //if thre's any
                ? CustomerHelper.ChooseCountryQuestion(holder.Countries.First(), holder) //choose one
                : GeneralHelpers.ChooseOrCreateCountry(holder.Countries); //else create one

            //if its new
            //new one have a id of 0
            if (chosenCountry.Id == 0)
            {
                _repo.AddCountry(chosenCountry);
                _repo.TrySaveChanges();
                chosenCountry = _repo.GetCountries().First(c => c.Name == chosenCountry.Name);
            }

            // Choose city, like country, by a city is in  acountry
            City chosenCity = holder.Cities.Any(c => c.CountryId == chosenCountry.Id) //same country
                ? CustomerHelper.ChooseCityQuestion(holder.Cities.First(c => c.CountryId == chosenCountry.Id), holder) //choose one of abailable cities
                : GeneralHelpers.ChooseOrCreateCity(holder.Cities, chosenCountry); //or create a new one
            //new city
            //add it
            if (chosenCity.Id == 0)
            {
                chosenCity.Country = chosenCountry;
                chosenCity.CountryId = chosenCountry.Id;
                _repo.AddCity(chosenCity);
                _repo.TrySaveChanges();
                holder.Cities.Add(chosenCity);
            }

            //we've created at least one city and country
            // Add address
            var newAddress = CustomerHelper.NewAdressQuestionnaire(chosenCity, holder);
            customer.CustomerShippingInfos.Add(newAddress);
            _repo.TrySaveChanges();
            Console.WriteLine("New address added!");
        }
        public List<BasketProduct> HandleCustomerBasket(int customerId)
        {
            var thisCustomer = _repo.GetCustomersQuired()
                .Include(q => q.ProductsInBasket)
                .ThenInclude(bp => bp.Product)
                .FirstOrDefault(x => x.Id == customerId);

            if (thisCustomer == null) return new List<BasketProduct>();

            BasketProduct bask = null;
            if (thisCustomer.ProductsInBasket.Count == 1)
                bask = thisCustomer.ProductsInBasket.First();
            else
                bask = StoreHelper.ChooseWhichBasketItem(thisCustomer.ProductsInBasket);

            if (bask != null)
                StoreHelper.AdjustQuantityOfBasketItems(bask);

            _repo.TrySaveChanges();

            // Return the same in-memory list
            return thisCustomer.ProductsInBasket.ToList();
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
        public void SaveNewCustomer(CustomerAccount cus)
        {
            _repo.SaveNewCustomer(cus);
        }
        public void AddProductToBasket(StoreProduct storeProduct, CustomerAccount cus)
        {

            Console.WriteLine("How many do you wish to add to your basket?");
            Console.WriteLine("Max quantity is " + storeProduct.Stock);
            int count = GeneralHelpers.StringToInt(Console.ReadLine());
            if (count > 0 && count <= storeProduct.Stock)
            {
                Console.WriteLine("Valid");
            }
            else
            {
                //Utgå från att någon bara köper en sak åt gången
                count = 1;
            }
            int prodId = storeProduct.Id;

            _repo.AddProductToBasket(prodId, count, cus);
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
