using ComputerStoreApplication.Account;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.Store;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Customer
{
    internal class CustomerHelper
    {
        public Dictionary<ConsoleKey, CRUD> keyValuePairs;
      
        private static readonly Dictionary<ConsoleKey, CRUD> Commandos = new()
        {
            {  ConsoleKey.C, CRUD.Create },
            { ConsoleKey.D, CRUD.Delete },
            { ConsoleKey.U, CRUD.Update },
        };

        public enum CRUD
        {
            Create = ConsoleKey.C, //Skapa product
            Update = ConsoleKey.U, //Uppdatera som i att vi ändrar värden
            Delete = ConsoleKey.D, //Ta bort, duh
        }
        internal static string RandomFirstName()
        {
            string[] firstNames =
           {
                // Swed (20)
                "Erik", "Johan", "Anna", "Maria", "Lars", "Karin", "Anders", "Eva", "Joel",
                "Per", "Sara", "Karl", "Emma", "Mats", "Lisa", "Henrik", "Julia",
                "Fredrik", "Sofia", "Daniel",

                // Amer (20)
                "Michael", "Jessica", "John", "Emily", "David", "Ashley", "James", "Hannah",
                "Robert", "Olivia", "William", "Ava", "Matthew", "Chloe", "Christopher", "Grace",
                "Andrew", "Megan", "Joshua", "Abigail",

                // Mid east (20)
                "Ahmed", "Fatima", "Ali", "Layla", "Omar", "Aisha", "Yusuf", "Zara",
                "Hassan", "Noor", "Ibrahim", "Mariam", "Samir", "Leila", "Khalid", "Rania",
                "Tariq", "Salma", "Nadia", "Amir"
            };
            int fNameIndex = Random.Shared.Next(firstNames.Length);
            string firstName = firstNames[fNameIndex];
            return firstName;
        }
        internal static string RandomSurName()
        {
            string[] surNames =
      {
            // Swedi (20)
            "Stormsten", "Eriksson", "Johansson", "Andersson", "Karlsson", "Nilsson",
            "Larsson", "Olsson", "Persson", "Svensson", "Gustafsson", "Pettersson",
            "Lindberg", "Lundqvist", "Bergström", "Lindgren", "Sandberg", "Hansson",
            "Berg", "Holm",

            // American (20)
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Wilson",
            "Taylor", "Anderson", "Thomas", "Moore", "Jackson", "White", "Harris",
            "Martin", "Thompson", "Garcia", "Martinez", "Clark",

            // Mid east (20)
            "Al-Farsi", "Haddad", "Rahman", "Khatib", "Darwish", "Najjar", "Saidi", "Mansour",
            "Aziz", "Saleh", "Nasser", "Abboud", "Tariq", "Hakim", "Farouk", "Zayed",
            "Shadid", "Hosseini", "Bakir", "Al-Mansouri"
        };

            int sNameIndex = Random.Shared.Next(surNames.Length);
            string surName = surNames[sNameIndex];

            return surName;

        }
        internal static string RandomPassword()
        {
            return Guid.NewGuid().ToString();
        }

        internal static string RandomEmail(string fName, string sName)
        {
            int randomNumber = Random.Shared.Next(1, 256);
            string email = $"{fName.ToLower()}.{sName.ToLower()}{randomNumber}@omail.com";

            return email;
        }
        internal static string RandomPhoneNumber()
        {
            string phoneNUmber = "07" + Random.Shared.Next(0, 9).ToString() + "-";
            for (int i = 0; i < 7; i++)
            {
                int rando = Random.Shared.Next(0, 9);
                phoneNUmber += rando.ToString();
            }
            return phoneNUmber;
        }
        public static void HandleDelivery(CustomerAccount cus)
        {

        }
        public static Order HandlePurchase(CustomerAccount customer)
        {
            if (customer == null)
            {
                return null;
            }
            if (customer.ProductsInBasket == null)
            {
                return null;
            }

            var newOrder = new Order
            {
                CustomerId = customer.Id,
                CreationDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };
            foreach (var item in customer.ProductsInBasket)
            {
                if (item.Quantity == 0 || item.Quantity <= 0)
                {
                    continue;
                }
                if (item.ComputerPart == null)
                {
                    Console.WriteLine($"Item {item} {item.ComputerPartId} failed to load, skipping");
                    continue;
                }

                newOrder.OrderItems.Add
                    (
                        new OrderItem
                        {
                            ComputerPartId = item.ComputerPartId,
                            Quantity = item.Quantity,
                            Price = item.ComputerPart.Price,
                        }
                    );

            }
            return newOrder;
        }
        public static bool CheckIfEmailFormat(string emial)
        {
            //hämtade regex mönster här
            //https://chatgpt.com/share/6979fd52-135c-800b-93d5-cdbf500a6fe2
            string pattern = @"^[a-zA-Z0-9åäöÅÄÖ._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isValid = Regex.IsMatch(emial, pattern);
            return isValid;
        }

        public static void HandleShippingInfo(ICollection<CustomerShippingInfo> customerShippingInfos, City city, LocationHolder locationHolder)
        {
            //can only create if we have no shipping info
            bool allowMultipleActions = customerShippingInfos.Count>0;
            if (allowMultipleActions)
            {
                allowMultipleActions = true;
                Console.WriteLine("Current registered shipping infos on your account");
                foreach (var customer in customerShippingInfos)
                {
                    Console.WriteLine($"{customer.StreetName}, {customer.PostalCode} {customer.State_Or_County_Or_Province}, {customer.City}");
                }
            }
            else
            {
                Console.WriteLine("No postal information added!");
            }
            if (allowMultipleActions)
            {
                foreach (var key in Commandos)
                {
                    Console.WriteLine($"[{key.Key}] to {key.Value} an address");
                }
            }
            else
            {
                Console.WriteLine("[C] to Create a new address");
            }

            var userInputC = Console.ReadKey(true);
            if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
            {
                Console.WriteLine("Some error here bud");
                return;
            }
            if (allowMultipleActions)
            {
                var selectedAdress = ChooseAdress(customerShippingInfos);
                if (userCrudValue != CRUD.Create)
                {
                    switch (userCrudValue)
                    {
                        case CRUD.Update:
                            if (selectedAdress != null)
                            {
                                AdressQuestionnaire(selectedAdress, city, locationHolder);
                            }
                            break;
                        case CRUD.Delete:
                            if (selectedAdress != null)
                            {
                                customerShippingInfos.Remove(selectedAdress);
                            }
                            break;
                    }
                }
            }
            else
            {
                CustomerShippingInfo newAddress = NewAdressQuestionnaire(city, locationHolder);
                customerShippingInfos.Add(newAddress);
            }
        }
        internal static void MakeSureOfShippingInfoLocation(CustomerShippingInfo shippingInfo,  LocationHolder locationHolder)
        {
            if (shippingInfo.City == null)
                shippingInfo.City = locationHolder.Cities.FirstOrDefault(c => c.Id == shippingInfo.CityId);

            if (shippingInfo.City?.Country == null && shippingInfo.City != null)
                shippingInfo.City.Country = locationHolder.Countries.FirstOrDefault(c => c.Id == shippingInfo.City.CountryId);
        }
        internal static CustomerShippingInfo ChooseAdress(ICollection<CustomerShippingInfo> customerShippingInfos)
        {
            Console.WriteLine("Which of these addresses? Choose by inputting the correct Id of the corresponding address");
            foreach (var customerShippingInfo in customerShippingInfos)
            {
                Console.WriteLine($"Id: {customerShippingInfo.Id} {customerShippingInfo.StreetName} {customerShippingInfo.PostalCode} etc");
            }
            int choice = GeneralHelpers.StringToInt();
            var validAdress = customerShippingInfos.FirstOrDefault(x => x.Id == choice);
            if (validAdress != null)
            {
                return validAdress;
            }
            else
            {
                return null;
            }
        }
        public static Country ChooseCountryQuestion(Country country, LocationHolder locationHolder)
        {
            bool sameCountry = false;
            Console.WriteLine($"Use country {country.Name}, or change?");
            sameCountry = GeneralHelpers.YesOrNoReturnBoolean();
            if (sameCountry)
            {
                return country;
            }
            else
            {
                return null;
                //return GeneralHelpers.ChooseOrCreateCountry(locationHolder.Countries);
            }
        }
        public static City ChooseCityQuestion(City city, LocationHolder locationHolder)
        {
            Console.WriteLine($"Use city {city.Name}, or change?");
            bool sameCity = GeneralHelpers.YesOrNoReturnBoolean();
            if (sameCity)
            {
                return city;
            }
            else
            {
                return null;
                //return GeneralHelpers.ChooseOrCreateCity(locationHolder.Cities, city.Country);
            }
        }
        public static void EditCustomerAccount(CustomerAccount accountToBeEdited)
        {
            Console.WriteLine("Firstname? Leave empty for no change");
            string fname = Console.ReadLine();
            if (!string.IsNullOrEmpty(fname))
            {
                accountToBeEdited.FirstName = fname;
            }
            Console.WriteLine("Surname? Leave empty for no change");
            string sname = Console.ReadLine();
            if (!string.IsNullOrEmpty(sname))
            {
                accountToBeEdited.SurName = sname;
            }
            Console.WriteLine("Email? Leave empty for no change");
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email))
            {
                accountToBeEdited.Email = email;
            }

            Console.WriteLine("Phone number? Leave empty for no change");
            string phonenumber = Console.ReadLine();
            if (!string.IsNullOrEmpty(phonenumber))
            {
                accountToBeEdited.PhoneNumber = phonenumber;
            }
        }
        internal static void AdressQuestionnaire(CustomerShippingInfo customerShippingInfo, City city, LocationHolder locationHolder)
        {
            Console.WriteLine("Streetname?");
            customerShippingInfo.StreetName = Console.ReadLine();

            Console.WriteLine("Postal code?");
            customerShippingInfo.PostalCode = GeneralHelpers.StringToInt();

            Console.WriteLine("Province/State?");
            customerShippingInfo.State_Or_County_Or_Province = Console.ReadLine();

            //Country chosenCountry = ChooseCountryQuestion(city.Country,locationHolder);
            //City chosenCity = GeneralHelpers.ChooseOrCreateCity(locationHolder.Cities, chosenCountry);

            //customerShippingInfo.City = chosenCity;
            //customerShippingInfo.CityId = chosenCity.Id;
            //Console.WriteLine("Updated address");
        }
        internal static CustomerShippingInfo NewAdressQuestionnaire(City city,LocationHolder locationHolder)
        {
            //Country chosenCountry = ChooseCountryQuestion(city.Country, locationHolder);
            //City chosenCity = GeneralHelpers.ChooseOrCreateCity(locationHolder.Cities, chosenCountry);
            Console.WriteLine("Streetname?");
            string street = Console.ReadLine();

            Console.WriteLine("Postal code?");
            int postal = GeneralHelpers.StringToInt();

            Console.WriteLine("Province/State?");
            string province = Console.ReadLine();

            //CustomerShippingInfo customerShippingInfo = new CustomerShippingInfo
            //{
            //    City = chosenCity,
            //    CityId = chosenCity.Id,
            //    StreetName = street,
            //    PostalCode = postal,
            //    State_Or_County_Or_Province = province
            //};
            //Console.WriteLine("Saved new address");
            //return customerShippingInfo;
            return null;

        }

        internal static void AdjustBasketItems(List<BasketProduct> basketProducts, ApplicationManager app)
        {
            var customer = app.GetCustomers().FirstOrDefault(x => x.Id == app.CustomerId);

            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                Console.ReadLine();
                return;
            }
            if (basketProducts.Count == 0 || basketProducts == null)
            {
                Console.WriteLine("Items not found");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("How many do you of this product?");
            int count = GeneralHelpers.ReturnValidIntOrNone();
            if (count == 0)
            {

            }


        }
    }
}
