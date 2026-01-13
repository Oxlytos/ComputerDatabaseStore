using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class StoreProduct
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public int ProductId { get; set; }

        public int? PartTypeId { get; set; }
        public virtual ComputerPart? PartType { get; set; }

        public int? ManufacturerId { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public decimal Price { get; set; }

        public bool Sale { get; set; }

        public int Stock {  get; set; }
    }
}
