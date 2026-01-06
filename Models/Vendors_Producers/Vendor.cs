using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ComputerStoreApplication.Models.ComputerComponents;

namespace ComputerStoreApplication.Models.Vendors_Producers
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<CPU> CPUs = new List<CPU>();
    }
}