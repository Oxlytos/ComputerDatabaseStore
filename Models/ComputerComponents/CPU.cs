using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public class CPU : ComputerPart
    {
        //https://stackoverflow.com/questions/5542864/how-should-i-declare-foreign-key-relationships-using-code-first-entity-framework

        //https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwith-nrt

        public decimal? MemorySpeedGhz { get; set; }

        public int? Cores { get; set; }

        public int? Threads { get; set; }

        public int? SocketId { get; set; }
        public virtual CPUSocket? SocketType { get; set; }

        public int? CPUArchitectureId { get; set; }
        public virtual CPUArchitecture? CPUArchitecture { get; set; }

        public bool Overclockable { get; set; }

        public decimal? CPUCache { get; set; }

        public CPU() { }
    }
}
