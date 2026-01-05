using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Customer
{
    internal class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int CustomerShippingInfoId { get; set; }

        public virtual List<CustomerShippingInfo> CustomerShippingInfo { get; set; } //A customer can have multiple saved addreses
    }
}
