using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    internal class GPU : ComputerPartType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; } //MSI, ASUS 

        [Required]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; } //Intel, AMD RX

        [Required]

        public int MemoryTypeId { get; set; }

        public virtual MemoryType MemoryType { get; set; }

        public int MemorySizeGB { get; set; }

        public decimal MemorySpeed {  get; set; }

        public bool Overclock {  get; set; }

        public int RequiredWattage { get; set; }


    }
}
