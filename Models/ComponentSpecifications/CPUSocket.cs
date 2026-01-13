using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComponentSpecifications
{
    public class CPUSocket
    {
        public int Id { get; set; }
        [Required]
        [StringLength(16)]
        public string CPUSocketName { get; set; }

        public ICollection<CPU> CPUs { get; set; } = new List<CPU>();

        public ICollection<Motherboard> Motherboards { get; set; } = new List<Motherboard>();
    }
}
