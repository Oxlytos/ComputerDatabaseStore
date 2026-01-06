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
    public class Motherboard : ComputerPartType
    {

        public int? ManufacturerId { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public int? CPUSocketId { get; set; }

        public virtual CPUSocket? CPUSocket { get; set; }
        public int? CPUArchitectureId { get; set; }

        public virtual CPUArchitecture? CPUSocketArchitecture { get; set; }
        public int? MemoryTypeId { get; set; }
        public virtual MemoryType? MemoryType { get; set; }
        public bool Overclockable { get; set; }
        public bool Bluetooth { get; set; }
        public bool Wifi { get; set; }
        public bool Soundcard { get; set; }
    }
}
