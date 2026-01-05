using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    internal class PSU : ComputerPartType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public decimal Wattage {  get; set; }

        public int EnergyClassId { get; set; }

        public virtual EnergyClass EnergyClass { get; set; }
    }
}
