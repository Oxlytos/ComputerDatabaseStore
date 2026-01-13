using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComponentSpecifications
{
    public class RamProfileFeatures
    {
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string RamProfileFeaturesType { get; set; } //XMP, EXPO

        public ICollection<RAM> RAMs { get; set; } = new List<RAM>(); 
    }
}
