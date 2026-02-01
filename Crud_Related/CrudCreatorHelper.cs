using ComputerStoreApplication.Account;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComputerStoreApplication.Crud_Related.CrudHandler;

namespace ComputerStoreApplication.Crud_Related
{
    public class CrudCreatorHelper
    {
        public static ComponentCategory CreateCategory()
        {
            ComponentCategory newComponentCategory = new ComponentCategory();
            Console.WriteLine("Name of this cateogory?");
            newComponentCategory.Name = GeneralHelpers.SetName(100);

            return newComponentCategory;
        }
        public static void ReadCategoryData(ComponentCategory part)
        {
            //Print stats
            //How many products are in this category
            //etc

        }

        internal static void UpdateCategory(ComponentCategory part)
        {
            Console.WriteLine("Leave empty to keep current name");
            string name = GeneralHelpers.ChangeName(part.Name, 30);
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            part.Name = name;

        }
        internal static DeliveryProvider CreateDeliveryProvider() 
        {
            DeliveryProvider deliveryProvider = new DeliveryProvider();
            Console.WriteLine("Name?");
            deliveryProvider.Name = GeneralHelpers.SetName(20);
            Console.WriteLine("Price for service (per order)");
            deliveryProvider.Price = GeneralHelpers.StringToDecimal();
            Console.WriteLine("Avg. Delivery time (not including weekends)");
            deliveryProvider.AverageDeliveryTime = GeneralHelpers.StringToInt();
            
            return deliveryProvider;
        }

        internal static void UpdateDeliverService(DeliveryProvider thing)
        {
            Console.WriteLine("To keep current properties, leave empty");
            Console.WriteLine("Name?");
            string name = GeneralHelpers.ChangeName(thing.Name, 30);
            if (!string.IsNullOrEmpty(name)) 
            {
            thing.Name = name;
            }
            Console.WriteLine("Price for service (per order)");
            thing.Price = GeneralHelpers.ChangeDecimal(thing.Price);
            Console.WriteLine("Avg. Delivery time (not including weekends)");
            thing.AverageDeliveryTime = GeneralHelpers.ChangeInt(thing.AverageDeliveryTime);
        }

        internal static ComputerPart CreateComputerPart(List<ComponentCategory> categories, List<Brand> brands)
        {
            ComputerPart newPart = new ComputerPart();
            Console.WriteLine("Name of this product");
            newPart.Name = GeneralHelpers.SetName(80);
            Console.WriteLine("Description");
            newPart.Description = Console.ReadLine();

            Console.WriteLine("Category?");
            newPart.CategoryId = GeneralHelpers.ChooseCategoryById(categories);

            Console.WriteLine("Brand/Manufacturer?");
            newPart.BrandId = GeneralHelpers.ChooseManufacturerById(brands);

            Console.WriteLine("Market this product as 'selected' on the front page?");
            newPart.SelectedProduct = GeneralHelpers.YesOrNoReturnBoolean();

            Console.WriteLine("Price (€), can include decimals");
            newPart.Price = GeneralHelpers.StringToDecimal();

            Console.WriteLine("Stock of this product, how many we got in store?");
            newPart.Stock = GeneralHelpers.StringToInt();

            Console.WriteLine("Is this product on sale?");
            newPart.Sale = GeneralHelpers.YesOrNoReturnBoolean();
            return newPart;



        }
        internal static void ReadProductData(ApplicationManager logic, ComputerPart thing)
        {
            logic.Dapper.TotalStockValueOnSpecificProduct(thing);


        }

        internal static void UpdateProduct(ComputerPart thing, List<Brand> allManufactuers, List<ComponentCategory> categories)
        {
            Console.WriteLine("Updating product...");
            string name = GeneralHelpers.ChangeName(thing.Name, 80);
            if (!string.IsNullOrEmpty(name))
            {
                thing.Name = name;
            }
            Console.WriteLine("Description, leave empty to keep current");
            string descirption = Console.ReadLine();
            if (!string.IsNullOrEmpty(descirption))
            {
                thing.Description = descirption;
            }
            Console.WriteLine("Category");
            ComponentCategory cat = GeneralHelpers.ChangeCategory(thing.ComponentCategory, categories);
            thing.CategoryId = cat.Id;
            Console.WriteLine("Then a price in € (can include decimals '.' ");
            string price = Console.ReadLine();
            if (!string.IsNullOrEmpty(price))
            {
                bool valid = decimal.TryParse(price, out decimal validDecimal);
                if (valid)
                {
                    thing.Price = validDecimal;
                }
                else
                {
                    thing.Price = GeneralHelpers.StringToDecimal();
                }
            }

            Brand brand = allManufactuers.FirstOrDefault(x => x.Id == thing.BrandId);
            if (brand != null)
            {
                Console.WriteLine("Current brand is " + brand.Name);
                brand = GeneralHelpers.ChangeManufacturer(brand, allManufactuers);
                thing.BrandId = brand.Id;
            }
            else
            {
                Console.WriteLine("Assign this product a manufacturer");
                thing.BrandId = GeneralHelpers.ChooseManufacturerById(allManufactuers);
            }

            Console.WriteLine("Is it one sale?");
            thing.Sale = GeneralHelpers.ChangeYesOrNo(thing.Sale);
            Console.WriteLine("How many we got in stock?");
            thing.Stock = GeneralHelpers.ChangeInt(thing.Stock);
            Console.WriteLine("Is this a 'selected product' we want to show on the front page and such?");
            thing.SelectedProduct = GeneralHelpers.ChangeYesOrNo(thing.SelectedProduct);
        }


        internal static void UpdatePaymentMethod(PaymentMethod thing)
        {
            Console.WriteLine("If you want to skip updating a field, leave it empty");
            thing.Name = GeneralHelpers.ChangeName(thing.Name, 20);
        }

        internal static void ReadPaymentData(ApplicationManager logic, PaymentMethod thing)
        {
            throw new NotImplementedException();
        }

        internal static PaymentMethod CreatePaymentMethod()
        {
            PaymentMethod pay = new PaymentMethod();
            Console.WriteLine("Name?");
            pay.Name = GeneralHelpers.SetName(20);
            return pay;
        }

        internal static void ReadCustomerData(ApplicationManager app, CustomerAccount thing)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateCustomerData(CustomerAccount customer)
        {
            Console.WriteLine("Firstname? Leave empty for no change");
                    customer.FirstName = GeneralHelpers.ChangeName(customer.FirstName, 30);
            Console.WriteLine("Surname? Leave empty for no change");
            customer.SurName= GeneralHelpers.ChangeName(customer.SurName, 30);
            Console.WriteLine("Email? Leave empty for no change");
           customer.Email= GeneralHelpers.ChangeName(customer.Email, 50);
            Console.WriteLine("Phone number? Leave empty for no change");
            customer.PhoneNumber = GeneralHelpers.ChangeName(customer.PhoneNumber, 15);
        }

        internal static void UpdateAddress(CustomerShippingInfo toUpdate, LocationHolder locationHolder)
        {
            Console.WriteLine("Leave empty to keep current");
            Console.WriteLine("Streetname?");
            toUpdate.StreetName = GeneralHelpers.ChangeName(toUpdate.StreetName,50);
            Console.WriteLine("Postal code?");
            toUpdate.PostalCode = GeneralHelpers.ChangeInt(toUpdate.PostalCode);
            Console.WriteLine("State/Province?");
            toUpdate.State_Or_County_Or_Province=GeneralHelpers.ChangeName(toUpdate.State_Or_County_Or_Province, 40);

            Console.WriteLine("Country?");
            Country country = GeneralHelpers.ChooseOrCreateCountry(locationHolder);
            City city = GeneralHelpers.ChooseOrCreateCity(locationHolder, country);
            if (toUpdate.City == null || toUpdate.City.Id != city.Id)
            {
                toUpdate.City = city;
            }
        }

        internal static CustomerShippingInfo CreateAddress(ApplicationManager app, LocationHolder locationHolder)
        {
            CustomerShippingInfo customerShippingInfo = new CustomerShippingInfo();
            Console.WriteLine("Street name?");
            customerShippingInfo.StreetName = GeneralHelpers.SetName(80);
            Console.WriteLine("Postal code");
            customerShippingInfo.PostalCode = GeneralHelpers.StringToInt();
            Console.WriteLine("Province/State?");
            customerShippingInfo.State_Or_County_Or_Province = GeneralHelpers.SetName(40);

            Country country = GeneralHelpers.ChooseOrCreateCountry(locationHolder);
            

            City city = GeneralHelpers.ChooseOrCreateCity(locationHolder, country);

            var countryExists = app.ComputerPartShopDB.Countries.FirstOrDefault(c => c.Name.ToLower() == country.Name.ToLower());
            if (countryExists == null)
            {
                city.Country = country;
                app.ComputerPartShopDB.Countries.Add(country);
                app.ComputerPartShopDB.SaveChanges();
            }
            else
            {
                country = countryExists;
                city.Country= country;
            }

            var cityExists = app.ComputerPartShopDB.Cities.FirstOrDefault(c => c.Name.ToLower() == city.Name.ToLower()&& c.CountryId == country.Id);

            if (cityExists == null)
            {
                app.ComputerPartShopDB.Cities.Add(city);
                app.ComputerPartShopDB.SaveChanges();
            }
            else
            {
                city = cityExists;
                city.Country = country;
            }
           
            customerShippingInfo.City = city;

            return customerShippingInfo;
        }

        internal static void ReadAddressInfo(CustomerShippingInfo adress, LocationHolder locationHolder)
        {
        }

        internal static void AdjustBasketItems(CustomerAccount currentCustomer, ApplicationManager logic)
        {
            var basketProducts = logic.GetBasketProductsFromCustomerId(logic.CustomerId);
            if (currentCustomer == null)
            {
                return;
            }
            if (basketProducts.Count == 0)
            {
                Console.WriteLine("No basket products found, returning...");
                Console.ReadLine();
                return;
            }

            var storeProducts = logic.GetStoreProducts();
            if (basketProducts.Count > 1)
            {
                Console.WriteLine("Which basket item? Input their Id, cancel operation by submitting 0");
                foreach (var product in basketProducts)
                {
                    Console.WriteLine($"Id: {product.Id} {product.ComputerPart.Name} Quantity: {product.Quantity}");
                }
                int choice = GeneralHelpers.ReturnValidIntOrNone();
                if (choice == 0)
                {
                    return;
                }
                var basketItem = basketProducts.FirstOrDefault(X => X.Id == choice);
                if (basketItem == null)
                {
                    Console.WriteLine("Error with basket item, returning...");
                    return;
                }
                var storeItem = storeProducts.FirstOrDefault(x => x.Id == basketItem.ComputerPartId);
                if(storeItem == null)
                {
                    Console.WriteLine("Error finding store item, returning...");
                    return;
                }
                Console.WriteLine($"How many of this product? Maximum is {storeItem.Stock}");
                int amount = GeneralHelpers.ReturnValidIntOrNone();
                if (amount > storeItem.Stock)
                {
                    Console.WriteLine("Input is greater than available stock, we've capped it at available amount");
                    basketItem.Quantity = storeItem.Stock;
                }
                if (amount == 0)
                {
                    logic.ComputerPartShopDB.Remove(basketItem);
                    logic.ComputerPartShopDB.SaveChanges();
                }
                else
                {
                    basketItem.Quantity=amount;
                }
            }
            else
            {
                var basketItem = basketProducts.First();
                var storeItem = storeProducts.FirstOrDefault(x => x.Id == basketItem.ComputerPartId);
                Console.WriteLine($"How many of this product? Maximum is {storeItem.Stock}");
                int amount = GeneralHelpers.ReturnValidIntOrNone();
                if (amount > storeItem.Stock)
                {
                    Console.WriteLine("Input is greater than available stock, we've capped it at available amount");
                    basketItem.Quantity = storeItem.Stock;
                }
                if (amount == 0)
                {
                    logic.ComputerPartShopDB.Remove(basketItem);
                    logic.ComputerPartShopDB.SaveChanges();
                }
                else
                {
                    basketItem.Quantity = amount;
                }

            }
            return;

        }

        internal static void ReadBrandStatistics(ApplicationManager manager, Brand validBrand)
        {

        }

        internal static void UpdateBrandName(Brand validBrand)
        {
            validBrand.Name = GeneralHelpers.ChangeName(validBrand.Name, 30);
        }

        internal static Brand CreateBrand()
        {
            Brand brand = new Brand();
            brand.Name = GeneralHelpers.SetName(30);
            return brand;
        }
    }
}
