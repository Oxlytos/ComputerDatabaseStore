using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComponentSpecifications
{
    internal class RamProfileFeatures
    {
        public int Id { get; set; }

        public string Type { get; set; } //XMP, EXPO

        public bool Overclockable { get; set; } //Can it be overclocked
    }
}
