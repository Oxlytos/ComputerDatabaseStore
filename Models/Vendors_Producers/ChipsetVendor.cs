using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ComputerStoreApplication.Models.ComputerComponents;

namespace ComputerStoreApplication.Models.Vendors_Producers
{
    public class ChipsetVendor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<ComputerPart> Parts { get; set; } = new List<ComputerPart>();
    }
}