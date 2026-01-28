using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.Store;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Customer
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        //Sällan längre
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string SurName { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string PhoneNumber { get; set; }

        public string Password { get; private set; }

        public virtual ICollection<CustomerShippingInfo> CustomerShippingInfos { get; set; }  = new List<CustomerShippingInfo>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<BasketProduct> ProductsInBasket { get; set; } = new List<BasketProduct>();

        public void CreatePassword()
        {
            if(Password.IsNullOrEmpty())
            {
                Password = Guid.NewGuid().ToString();
            }
        }
        public CustomerShippingInfo NewAdress()
        {

            return null;
        }
        public void PrintShippingInfo()
        {
            var shipInfo = CustomerShippingInfos.ToList();
            if (shipInfo.Count > 0) 
            {
                foreach (var ship in shipInfo)
                {
                    Console.WriteLine($"{ship.StreetName} {ship.PostalCode} {ship.State_Or_County_Or_Province} {ship.Country}\n");
                }
            }
            else
            {
                Console.WriteLine("No shipping address registered");
            }
            
        }
        public  void PrintOrders()
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
