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
    public class GPU : ComputerPart
    {
        public int? MemoryTypeId { get; set; }

        public virtual MemoryType? MemoryType { get; set; }

        public int MemorySizeGB { get; set; }

        public decimal MemorySpeed {  get; set; }

        public bool Overclock {  get; set; }
        public int RecommendedPSUWattage { get; set; }
        public int WattageConsumption { get; set; }

        public GPU() { }

    }
}
