using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Customer
{
    public class CustomerShippingInfo
    {
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public int PostalCode { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State_Or_County_Or_Province { get; set; }
        public string Country { get; set; }
    }
}
