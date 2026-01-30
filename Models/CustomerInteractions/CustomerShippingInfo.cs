using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ComputerStoreApplication.Account;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerStoreApplication.Models.Store;


namespace ComputerStoreApplication.Models.Customer
{
    public class CustomerShippingInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public virtual CustomerAccount? Customer { get; set; }
        public int PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string StreetName { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        [Required]
        [StringLength(40)]
        public string State_Or_County_Or_Province { get; set; }

        internal CustomerShippingInfo() { }
        public CustomerShippingInfo(int postalcode, string streetname, City city, string province)
        {
            PostalCode = postalcode;
            StreetName = streetname;
            City = city;
            State_Or_County_Or_Province= province;
            
        }
    }
}
