using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.Store;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
        public static Customer CreateCustomerManual(string email)
        {
            Console.WriteLine("Firstname?");
            string firstName = Console.ReadLine();

            Console.WriteLine("Surname?");
            string surName = Console.ReadLine();

            Console.WriteLine("Phone number (exclude +46 in format '072-0702133'', include dash");
            string phoneNumber = Console.ReadLine() ?? string.Empty;
            Customer customer = new Customer
            {
                FirstName = firstName,
                SurName = surName,
                Email = email,
                PhoneNumber = phoneNumber,
            };
            return customer;
        }
        public static Customer CreateCustomer(Customer currCustomer)
        {
            string firstName = RandomFirstName();
            string surName = RandomSurName();
            Customer customer = new Customer
            {
                FirstName = firstName,
                SurName = surName,
                Email = RandomEmail(firstName, surName),
                PhoneNumber = CustomerHelper.RandomPhoneNumber(),
            };
            return customer;
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
        public static void HandleDelivery(Customer cus)
        {

        }
        public static Order HandlePurchase(Customer customer)
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
                    return null;
                }
                if (item.Product == null)
                {
                    Console.WriteLine($"Item {item} {item.ProductId} failed to load, skipping");
                    continue;
                }

                newOrder.OrderItems.Add
                    (
                        new OrderItem
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = item.Product.Price,
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
        public static void HandleShippingInfo(ICollection<CustomerShippingInfo> customerShippingInfos)
        {
            bool allowMultipleActions = false;
            if (customerShippingInfos.Count > 0)
            {
                allowMultipleActions = true;
                Console.WriteLine("Current registered shipping infos on your account");
                foreach (var customer in customerShippingInfos)
                {
                    Console.WriteLine($"{customer.StreetName}, {customer.PostalCode} {customer.State_Or_County_Or_Province}, {customer.City}, {customer.Country}");
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
                    Console.WriteLine($"[{key.Key}] to {key.Value} an adress");
                }
            }
            else
            {
                Console.WriteLine("[C] to Create a new adress");
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
                                AdressQuestionnaire(selectedAdress);
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
                CustomerShippingInfo newAddress = NewAdressQuestionnaire();
                customerShippingInfos.Add(newAddress);
            }
        }
        internal static CustomerShippingInfo ChooseAdress(ICollection<CustomerShippingInfo> customerShippingInfos)
        {
            Console.WriteLine("Which of these adresses? Choose by inputting the correct Id of the corresponding adress");
            foreach (var customerShippingInfo in customerShippingInfos)
            {
                Console.WriteLine($"Id: {customerShippingInfo.Id} {customerShippingInfo.StreetName} {customerShippingInfo.PostalCode} etc");
            }
            int choice = GeneralHelpers.StringToInt(Console.ReadLine());
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

        internal static void AdressQuestionnaire(CustomerShippingInfo customerShippingInfo)
        {
            Console.WriteLine("Streetname?");
            customerShippingInfo.StreetName = Console.ReadLine();

            Console.WriteLine("City?");
            customerShippingInfo.City = Console.ReadLine();

            Console.WriteLine("Postal code?");
            customerShippingInfo.PostalCode = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("Province/State?");
            customerShippingInfo.State_Or_County_Or_Province = Console.ReadLine();

            Console.WriteLine("Country?");
            customerShippingInfo.Country = Console.ReadLine();

        }
        internal static CustomerShippingInfo NewAdressQuestionnaire()
        {
            Console.WriteLine("Streetname?");
            string street = Console.ReadLine();

            Console.WriteLine("City?");
            string city = Console.ReadLine();

            Console.WriteLine("Postal code?");
            int postal = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("Province/State?");
            string province = Console.ReadLine();

            Console.WriteLine("Country?");
            string country = Console.ReadLine();
            CustomerShippingInfo customerShippingInfo = new CustomerShippingInfo
            {
                StreetName = street,
                City = city,
                PostalCode = postal,
                State_Or_County_Or_Province = province,
                Country = country
            };
            return customerShippingInfo;

        }
    }
}
