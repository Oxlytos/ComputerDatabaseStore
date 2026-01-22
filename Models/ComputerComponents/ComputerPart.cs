using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ComputerStoreApplication.Logic;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public abstract class ComputerPart
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; } = string.Empty;

        public string? Description = string.Empty;

        public int? BrandId { get; set; }

        public virtual Brand? BrandManufacturer { get; set; } //MSI, ASUS 

        public int? ChipsetVendorId { get; set; }

        public virtual ChipsetVendor? ChipsetVendor { get; set; } //Intel, AMD RX

        //Implement later use
        public ICollection<StoreProduct> Products { get; set; } = new List<StoreProduct>();

        public int? Stock {  get; set; }

        public abstract void Create(ApplicationManager lol);
        public abstract void Read(ApplicationManager lol);
        public abstract void Update(ApplicationManager lol);
        public abstract void Delete(ApplicationManager lo);

      
    }
}
