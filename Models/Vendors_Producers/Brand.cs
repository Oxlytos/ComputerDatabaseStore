using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Vendors_Producers
{
    public class Brand
    {
        public int Id { get; set; }

        [StringLength(40)]
        public string Name { get; set; } //Nvidia, AMD, Intel, Seagate

        //Manufacturers have products they sell, MSI has GPUs, Corsair has Ram sticks (RIP) and Intel CPUs, but they're all 
        //contextually Computer Parts (of different types)
        public ICollection<ComputerPart> Products { get; set; } = new List<ComputerPart>();
    }
}
