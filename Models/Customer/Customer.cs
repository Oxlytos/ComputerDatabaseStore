using ComputerStoreApplication.Models.Store;
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

        public int CustomerShippingInfoId { get; set; }

        public virtual List<CustomerShippingInfo> CustomerShippingInfo { get; set; } //A customer can have multiple saved addreses, or something, ship to work?

        public virtual List<CustomerOrder> Orders { get; set; }
    }
}
