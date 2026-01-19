using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public class PSU : ComputerPart
    {
        public decimal Wattage {  get; set; }

        public int? EnergyClassId { get; set; }

        public virtual EnergyClass? EnergyClass { get; set; }

        public PSU() { }

        public override void Create(ApplicationManager lol)
        {
            throw new NotImplementedException();
        }

        public override void Read(ApplicationManager lol)
        {
            throw new NotImplementedException();
        }

        public override void Update(ApplicationManager lol)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ApplicationManager lo)
        {
            throw new NotImplementedException();
        }
    }
}
