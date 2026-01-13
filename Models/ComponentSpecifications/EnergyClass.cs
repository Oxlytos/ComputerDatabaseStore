using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComponentSpecifications
{
    public class EnergyClass
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string EnergyNameClass { get; set; } //Bronze, Silver, Gold etc
        //From this
        //https://en.wikipedia.org/wiki/80_Plus

        public ICollection<PSU> PSUs { get; set; } = new List<PSU>();
    }
}
