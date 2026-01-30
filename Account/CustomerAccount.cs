using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Account
{
    public class CustomerAccount : AccountBase
    {
        protected CustomerAccount() { }
     
        public virtual ICollection<CustomerShippingInfo> CustomerShippingInfos { get; set; } = new List<CustomerShippingInfo>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<BasketProduct> ProductsInBasket { get; set; } = new List<BasketProduct>();
           public static CustomerAccount CreateCustomerManual(string email)
        {
            Console.WriteLine("Firstname?");
            string firstName = Console.ReadLine();

            Console.WriteLine("Surname?");
            string surName = Console.ReadLine();

            Console.WriteLine("Phone number (exclude +46 in format '072-0702133'', include dash");
            string phoneNumber = Console.ReadLine() ?? string.Empty;
            CustomerAccount customer = new CustomerAccount()
            {
                FirstName = firstName,
                SurName = surName,
                Email = email,
                PhoneNumber = phoneNumber,

            };
            customer.CreatePassword();
            return customer;
        }
        public static CustomerAccount CreateCustomer(CustomerAccount currCustomer)
        {
            string firstName = CustomerHelper.RandomFirstName();
            string surName = CustomerHelper.RandomSurName();
            CustomerAccount customer = new CustomerAccount()
            {
                FirstName = firstName,
                SurName = surName,
                Email = CustomerHelper.RandomEmail(firstName, surName),
                PhoneNumber = CustomerHelper.RandomPhoneNumber(),
            };
            customer.CreatePassword();
            return customer;
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
            string sname=Console.ReadLine();
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

            Console.WriteLine("Phonenumber? Leave empty for no change");
            string phonenumber = Console.ReadLine();
            if (!string.IsNullOrEmpty(phonenumber))
            {
                accountToBeEdited.PhoneNumber = phonenumber;
            }
        }
        public void PrintShippingInfo()
        {
            var shipInfo = CustomerShippingInfos.ToList();
            if (shipInfo.Count > 0)
            {
                foreach (var ship in shipInfo)
                {
                    Console.WriteLine($"{ship.StreetName} {ship.PostalCode} {ship.State_Or_County_Or_Province} {ship.City.Country}\n");
                }
            }
            else
            {
                Console.WriteLine("No shipping address registered");
            }

        }
        public void PrintOrders()
        {
            var cOrders = Orders.ToList();
            if (cOrders.Count > 0)
            {
                foreach (var ord in cOrders)
                {
                }
            }
            else
            {
                Console.WriteLine("No shipping address registered");
            }
        }
    }
}
