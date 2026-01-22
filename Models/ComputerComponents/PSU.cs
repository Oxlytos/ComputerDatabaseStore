using ComputerStoreApplication.Helpers;
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
            var manufacturers = lol.GetManufacturers();
            var energyClasses = lol.GetEnergyClasses();

            Console.WriteLine("Name of the PSU?");
            string name = Console.ReadLine();

            Brand manufacturer = GeneralHelpers.ChooseManufacturer(manufacturers);
           
            Console.WriteLine("Total Wattage?");
            decimal watt = GeneralHelpers.StringToDecimal(Console.ReadLine());
            EnergyClass eClass = GeneralHelpers.ChooseEnergyClass(energyClasses);

            Console.WriteLine("How many we got in stock of this new CPU?");
            int stock = GeneralHelpers.StringToInt(Console.ReadLine());

            PSU newPsu = new PSU
            {
                Name = name,
                BrandManufacturer = manufacturer,
                Wattage = watt,
                EnergyClass = eClass,
                Stock = stock,
            };
            lol.SaveNewComponent(newPsu);
        }

        public override void Read(ApplicationManager lol)
        {
            //Hämta alla properties
            var propertiers = this.GetType().GetProperties();
            Console.WriteLine($"Info on this {this.Name}");
            foreach (var prop in propertiers)
            {
                //Hämta value på denna property i loopen
                var value = prop.GetValue(this);
                string[] skips = GeneralHelpers.SkippablePropertiesInPrints();
                if (!skips.Contains(prop.Name))
                {
                    //Kolla specifika props, för formatering och att vi får deras properties korrekt
                    switch (value)
                    {
                        case Brand m:
                            Console.WriteLine($"{prop.Name} : {m.Name}");
                            break;
                        case ChipsetVendor v:
                            Console.WriteLine($"{prop.Name} : {v.Name}");
                            break;
                        case EnergyClass e:
                            Console.WriteLine($"{prop.Name} : {e.Name}");
                            break;
                        default:
                            Console.WriteLine($"{prop.Name} : {value}");
                            break;
                    }
                }
            }
            Console.ReadLine();
        }

        public override void Update(ApplicationManager lol)
        {
            Console.WriteLine("To update a field, input the corresponding name to edit it");
            Console.WriteLine("For example, want to edit name? Type in 'name' ");
            Console.WriteLine("Exit by submitting an empty answer");
            Read(lol);
            Console.WriteLine("Please, input the Name of the property you want to update");
            var propertiers = this.GetType().GetProperties();
            //Input
            string userInput = Console.ReadLine();
            string[] keywords = GeneralHelpers.SpecialFields();
            var selectedProperty = propertiers.FirstOrDefault(p => p.Name.ToLower() == userInput.ToLower());
            if (selectedProperty != null)
            {
                var propVal = selectedProperty.GetValue(this);
                Console.WriteLine($"Prop val name: {propVal.GetType().Name}");
                //Basic properties som ints, decimals, strängar
                if (!keywords.Contains(propVal.GetType().Name))
                {
                    var value = GeneralHelpers.TryAndUpdateValueOnObject(selectedProperty);
                    if (value != null)
                    {
                        selectedProperty.SetValue(this, value);
                        Console.WriteLine("Done! Press Enter");
                        lol.SaveChangesOnComponent();
                        Console.ReadLine();
                    }
                }
                //Här ändrar vi Vendor, Manufactuer IDs och annat
                else
                {
                    //Vi byter lokalt virtual property på vårat objekt
                    //Påverkar bara this
                    switch (propVal)
                    {
                        case Brand M:
                            var man = lol.GetManufacturers();
                            if (GeneralHelpers.ChangeManufacturer(man, this))
                            {
                                lol.SaveChangesOnComponent();
                                Console.ReadLine();
                            }
                            break;
                        case ChipsetVendor V:
                            var vend = lol.GetVendors();
                            if (GeneralHelpers.ChangeVendor(vend, this))
                            {
                                lol.SaveChangesOnComponent();
                                Console.ReadLine();
                            }
                            break;
                        case EnergyClass EC:
                            var classes = lol.GetEnergyClasses();
                            if (GeneralHelpers.ChangeEnergyClass(classes, this))
                            {
                                lol.SaveChangesOnComponent();
                                Console.ReadLine();
                            }
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Exiting edit mode...");
                Console.ReadLine();
            }
        }

        public override void Delete(ApplicationManager lo)
        {
            Console.WriteLine($"Do you really want to delete this {this.Name}? Affirm by inputting 'y' for yes, 'n' for no");
            bool userAnswer = GeneralHelpers.YesOrNoReturnBoolean(Console.ReadLine());
            if (userAnswer)
            {
                // Checka här om det går att ta bort på riktigt
                //Det ska inte gå att ta bort något om det kanske är en utvald produkt? Eller håller på att fraktas?
                //Validering för det
                //Sen ta bort
                Console.WriteLine("Deleting....");
                lo.RemoveComponent(this);
            }
        }
    }
}
