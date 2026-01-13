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
    public class RAM : ComputerPart
    {
        public int? MemoryTypeId { get; set; }

        public virtual MemoryType MemoryType { get; set; }

        public int MemorySizePerStick { get; set; }

        public decimal MemorySpeed {  get; set; }

        public ICollection<RamProfileFeatures> SupportedRamProfiles = new List<RamProfileFeatures>();
        public RAM() { }
    }

}
