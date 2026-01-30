using ComputerStoreApplication.Models.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class City
    {
        [Required]
        public int Id { get; set; }
        [StringLength(60)]
        public string Name { get; set; }

        public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }

        public virtual ICollection<CustomerShippingInfo> CustomerShippingInfos { get; set; } = new List<CustomerShippingInfo>();
    }
}
