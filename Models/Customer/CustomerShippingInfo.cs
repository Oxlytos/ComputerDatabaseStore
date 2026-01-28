using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Customer
{
    public class CustomerShippingInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public int PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string StreetName { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(40)]
        public string State_Or_County_Or_Province { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        internal CustomerShippingInfo() { }
        public CustomerShippingInfo(int postalcode, string streetname, string city, string province, string country)
        {
            PostalCode = postalcode;
            StreetName = streetname;
            City = city;
            State_Or_County_Or_Province= province;
            Country = country;
            
        }
    }
}
