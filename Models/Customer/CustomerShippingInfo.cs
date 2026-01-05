using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Customer
{
    internal class CustomerShippingInfo
    {
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public int PostalCode { get; set; }

        public string StreetName { get; set; }

        public string City { get; set; }

        public string StateCountyProvince { get; set; }

        public string Country { get; set; }
    }
}
