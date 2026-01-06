using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class StoreProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public int? PartTypeId { get; set; }
        public virtual ComputerPartType? PartType { get; set; }

        public int? ManufacturerId { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public float Price { get; set; }

        public bool Sale { get; set; }

        public int Stock {  get; set; }
    }
}
