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
    internal class CPU : ComputerPartType
    {

        //https://stackoverflow.com/questions/5542864/how-should-i-declare-foreign-key-relationships-using-code-first-entity-framework

        //https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwith-nrt
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } //AMD 7800x3d t.ex.

        [Required]
        public int VendorId { get; set; }
        
        public virtual Vendor Vendor { get; set; } //AMD, Nvidia

        public float MemorySpeedGhz { get; set; }

        public int Cores { get; set; }

        public int Thrads { get; set; }

        [Required]
        public int SocketId { get; set; }
        public virtual CPUSocket SocketType { get; set; }

        [Required]
        public int CPUArchitectureId { get; set; }
        public virtual CPUArchitecture CPUArchitecture { get; set; }

        public bool Overclockable { get; set; }

        public float? CPUCache {  get; set; }

        public CPU(string name, int vendorId, int cores, int threads, float memorySpeed, int socketId, int CPUArch, bool oc, float cache)
        {
            Name = name;
            VendorId = vendorId;
            Cores = cores;
            SocketId = socketId;
            CPUArchitectureId = CPUArch;
            Overclockable = oc;
            CPUCache = cache;
            Thrads = threads;
            MemorySpeedGhz = memorySpeed;

        }
    }
}
