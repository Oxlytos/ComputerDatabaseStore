using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public abstract class ComputerPart
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; } = string.Empty;

        public int? ManufacturerId { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; } //MSI, ASUS 

        public int? VendorId { get; set; }

        public virtual Vendor? Vendor { get; set; } //Intel, AMD RX

        //Implement later use
        public ICollection<StoreProduct> Products { get; set; } = new List<StoreProduct>();

        public int? Stock {  get; set; }
    }
}
