using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public class RAM : ComputerPart
    {
        public int? MemoryTypeId { get; set; }

        public virtual MemoryType MemoryType { get; set; }

        public int MemorySizePerStick { get; set; }

        public decimal MemorySpeed {  get; set; }

        public ICollection<RamProfileFeatures> SupportedRamProfiles = new List<RamProfileFeatures>();
        public RAM() { }

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
