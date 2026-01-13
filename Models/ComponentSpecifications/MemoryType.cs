using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComponentSpecifications
{
    public class MemoryType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string MemoryTypeName { get; set; }

        public ICollection<RAM> RAMs { get; set; } = new List<RAM>();
        public ICollection<Motherboard> Motherboards { get; set;} = new List<Motherboard>();
        public ICollection<GPU> GPUs { get; set; } = new List<GPU>();
    }
}
