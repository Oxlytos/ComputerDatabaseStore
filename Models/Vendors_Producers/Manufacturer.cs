using ComputerStoreApplication.Models.ComputerComponents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Vendors_Producers
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } //Nvidia, AMD, Intel, Seagate

        //Manufacturers have products they sell, MSI has GPUs, Corsair has Ram sticks RIP and Intel CPUs, but they're all 
        //contextually Computer Parts (of different types)
        public ICollection<ComputerPartType> Products { get; set; } = new List<ComputerPartType>();
    }
}
