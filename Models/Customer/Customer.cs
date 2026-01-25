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

        public virtual ICollection<CustomerShippingInfo> CustomerShippingInfo { get; set; } 

        public virtual ICollection<CustomerOrder> Orders { get; set; }

        public virtual ICollection<BasketProduct> ProductsInBasket { get; set; } = new List<BasketProduct>();

        public void CreatePassword()
        {
            if(Password.IsNullOrEmpty())
            {
                Password = Guid.NewGuid().ToString();
            }
        }
    }
}
