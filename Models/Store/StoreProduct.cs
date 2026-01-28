using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class StoreProduct
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        public Guid StoreProductId { get; set; }

        public string? Description { get; set; }

        public int ComputerPartId { get; set; }
        public virtual ComputerPart ComputerPart { get; set; }

        public int? ManufacturerId { get; set; }

        public virtual Brand? Manufacturer { get; set; }

        public decimal Price { get; set; }


        public bool Sale { get; set; }

        //Visa på hemma skärm och browse att detta är produkt i extra fokus => Fast som inte är på rea
        public bool SelectedProduct { get; set; }

        public int Stock {  get; set; }

        public void Create(ApplicationManager lol, ComputerPart part)
        {
            ComputerPartId = part.Id;
            ComputerPart = part;
            if (part.BrandManufacturer != null)
            {
                ManufacturerId = part.BrandId;
            }
            var manufacturers= lol.GetManufacturers();
            StoreProduct prod = new StoreProduct();
            prod = StandardFillOutQuestionnaire(manufacturers);
            lol.SaveNewStoreProduct(this);

        }
        public void Read(ApplicationManager lol)
        {
            //Gah
        }
        public void Update(ApplicationManager lol)
        {
            var manufacturers = lol.GetManufacturers();
            StandardFillOutQuestionnaire(manufacturers);
            lol.SaveChangesOnComponent();
        }
        public void Delete(ApplicationManager lol) 
        {
        
        }
        internal StoreProduct StandardFillOutQuestionnaire(List<Brand> manufacturers)
        {
            Name = GeneralHelpers.SetName(80);
            //https://stackoverflow.com/questions/2344098/c-sharp-how-to-create-a-guid-value
            StoreProductId = Guid.NewGuid();
            Console.WriteLine("Now a description");
            Description = Console.ReadLine();
            Console.WriteLine("Then a price in €");
            Price = GeneralHelpers.StringToDecimal(Console.ReadLine());
            Manufacturer = GeneralHelpers.ChooseManufacturer(manufacturers);
            Console.WriteLine("Is it one sale?");
            Sale = GeneralHelpers.YesOrNoReturnBoolean(Console.ReadLine());
            Console.WriteLine("How many we got in stock?");
            Stock = GeneralHelpers.StringToInt(Console.ReadLine());
            Console.WriteLine("Is this a 'selected product' we want to show on the front page and such?");
            SelectedProduct = GeneralHelpers.YesOrNoReturnBoolean(Console.ReadLine());
            return this;
        }
    }
}
