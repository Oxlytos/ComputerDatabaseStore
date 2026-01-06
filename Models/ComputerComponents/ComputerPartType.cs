using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public abstract class ComputerPartType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
    public enum PartTypes
    {
        CPU,
        GPU,
        RAM,
        PSU,
        Motherboard
    }
}
