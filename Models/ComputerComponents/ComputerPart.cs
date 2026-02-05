using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using DnsClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public class ComputerPart
    {
        public int Id { get; set; }

        [StringLength(80)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public virtual ComponentCategory? ComponentCategory { get; set; }

        public int? BrandId { get; set; }
        public virtual Brand? BrandManufacturer { get; set; } //MSI, ASUS 

        public bool SelectedProduct { get; set; }   
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool Sale { get; set; }

        public ComputerPart Create(List<Brand> brands, List<ComponentCategory> componentCategories)
        {
            Console.WriteLine("Name of this product");
            Name = GeneralHelpers.SetName(80);
            Console.WriteLine("Description");
            Description = Console.ReadLine();

            Console.WriteLine("Category?");
            CategoryId =  GeneralHelpers.ChooseCategoryById(componentCategories);

            Console.WriteLine("Brand/Manufacturer?");
            BrandId= GeneralHelpers.ChooseManufacturerById(brands);

            Console.WriteLine("Market this product as 'selected' on the front page?");
            SelectedProduct = GeneralHelpers.YesOrNoReturnBoolean();

            Console.WriteLine("Price (€), can include decimals");
            Price = GeneralHelpers.StringToDecimal();

            Console.WriteLine("Stock of this product, how many we got in store?");
            Stock = GeneralHelpers.StringToInt();

            Console.WriteLine("Is this product on sale?");
            Sale = GeneralHelpers.YesOrNoReturnBoolean();
            return this;
        }
        public void Read(Brand brand, ComponentCategory category)
        {
            Console.Clear();
            int left = 2;
            int top = 18;
            int middleOfTheScreen = GeneralHelpers.ReturnMiddleOfTheScreenXAxisWithOffsetForSomeStringOrLength(20);
            var rows = new List<string>
            {
                //Nicer formatting
                $"Id: {this.Id}".PadRight(40),
                " ",
                this.Name.PadRight(40),
                 " ",
                $"Price: {this.Price} €".PadRight(40),
                 " ",
                (this.Sale ? "ON SALE!" : "(Not on sale)").PadRight(40),
                 " ",
                $"Category: {category.Name}".PadRight(40),
                 " ",
                $"Brand/Manufacturer: {brand.Name}",
                 " ",
                $"This many left: {this.Stock}"
            };
            //Some text wrapping, longer chunks of texts (like description) gets broken up into chunks by max length
            foreach (var line in GeneralHelpers.TextWrapper(this.Description, 40))
            {
                rows.Add(line.PadRight(40));
            }
            var window = new MickesWindow.Window(
                     $"More info detailed view...",
                    50,
                     top,
                     rows
                 );
            //Draw X window
            window.Draw();


        }
        internal ComputerPart UpdateForm(List<Brand> manufacturers, List<ComponentCategory> componentCategories)
        {
            Console.WriteLine("If you want to skip updating a field, leave it empty");
            Console.WriteLine("Name of this product");
            string name = GeneralHelpers.ChangeName(Name, 80);
            if (!string.IsNullOrEmpty(name))
            {
                Name = name;
            }
            Console.WriteLine("Description");
            string descirption = Console.ReadLine();
            if (!string.IsNullOrEmpty(descirption))
            {
                Description = descirption;
            }
            Console.WriteLine("Category");
            ComponentCategory = GeneralHelpers.ChangeCategory(ComponentCategory, componentCategories);
            Console.WriteLine("Then a price in € (can include decimals '.' ");
            string price = Console.ReadLine();
            if (!string.IsNullOrEmpty(price))
            {
                bool valid = decimal.TryParse(price, out decimal validDecimal);
                if (valid)
                {
                    Price = validDecimal;
                }
                else
                {
                    Price = GeneralHelpers.StringToDecimal();
                }
            }
            if (BrandManufacturer != null)
            {
                BrandManufacturer = GeneralHelpers.ChangeManufacturer(BrandManufacturer, manufacturers);
            }
            else
            {
                Console.WriteLine("Assign this product a manufacturer");
                BrandId = GeneralHelpers.ChooseManufacturerById(manufacturers);
            }

            Console.WriteLine("Is it one sale?");
            Sale = GeneralHelpers.ChangeYesOrNo(Sale);
            Console.WriteLine("How many we got in stock?");
            Stock = GeneralHelpers.ChangeInt(Stock);
            Console.WriteLine("Is this a 'selected product' we want to show on the front page and such?");
            SelectedProduct = GeneralHelpers.ChangeYesOrNo(SelectedProduct);
            return this;
        }

    }
}
