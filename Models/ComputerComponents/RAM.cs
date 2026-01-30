using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using Microsoft.EntityFrameworkCore;
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

        public decimal MemorySpeed { get; set; }

        public virtual ICollection<RamProfileFeatures> SupportedRamProfiles { get; set; }
        public RAM() { }

        public override void Create(ApplicationManager lol)
        {
            var manufacturers = lol.GetManufacturers();
            var ramProfiles = lol.GetRamProfileFeatures();
            var memTypes = lol.GetMemoryTypes();
            Console.WriteLine("Name of the RAM collection?");
            string ramName = Console.ReadLine();


            Brand manufacturer = GeneralHelpers.ChooseManufacturer(manufacturers);
            List<RamProfileFeatures> features = GeneralHelpers.ChooseProfileFeatures(ramProfiles);
            MemoryType whatMemoryType = GeneralHelpers.ChooseMemoryType(memTypes);

            Console.WriteLine("GBs per stick?");
            int gbs = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("Memory Speed (MHz)?");
            decimal memSpeed = GeneralHelpers.StringToDecimal(Console.ReadLine());

            RAM newRam = new RAM
            {
                Name = ramName,
                BrandManufacturer = manufacturer,
                SupportedRamProfiles = features,
                MemorySizePerStick = gbs,
                MemorySpeed = memSpeed,
                MemoryType = whatMemoryType,

            };
            lol.SaveNewComponent(newRam);
        }

        public override void Read(ApplicationManager lol)
        {
            var memStick = lol.ComputerPartShopDB.AllParts.OfType<RAM>()
            .Include(r => r.SupportedRamProfiles)
            .Include(r => r.BrandManufacturer)
            .Include(r => r.MemoryType)
            .Include(r => r.ChipsetVendor) 
            .FirstOrDefault(x => x.Id == this.Id);
            if (memStick == null)
            {
                Console.WriteLine("Some error here, returning");
                return;
            }
            //Hämta alla properties
            var propertiers = memStick.GetType().GetProperties();
            Console.WriteLine($"More technical info {this.Name}");
            foreach (var prop in propertiers)
            {
                //Hämta value på denna property i loopen
                var value = prop.GetValue(memStick);
                if (value == null)
                {
                    continue;
                }
                string[] skips = GeneralHelpers.SkippablePropertiesInPrints();
                if (!skips.Contains(prop.Name))
                {
                    //Kolla specifika props, för formatering och att vi får deras properties korrekt
                    switch (value)
                    {
                        case Brand m:
                            Console.WriteLine($"- {prop.Name} : {m.Name}");
                            break;
                        case MemoryType m:
                            Console.WriteLine($"- {prop.Name} : {m.Name}");
                            break;
                        case ChipsetVendor cV:
                            continue;
                        case CPUArchitecture c:
                            Console.WriteLine($"- {prop.Name} : {c.Name}");
                            break;
                        case CPUSocket cs:
                            Console.WriteLine($"- {prop.Name} : {cs.Name}");
                            break;
                        case IEnumerable<RamProfileFeatures> profiles:
                            Console.WriteLine($"- {prop.Name}:");
                            foreach (var profile in profiles)
                            {
                                Console.WriteLine($"    - {profile.Name}");
                            }
                            break;
                        default:
                            Console.WriteLine($"- {prop.Name} : {value}");
                            break;
                    }
                }
            }
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
                        case MemoryType Mem:
                            var mems = lol.GetMemoryTypes();
                            if (GeneralHelpers.ChangeMemoryType(mems, this))
                            {
                                lol.SaveChangesOnComponent();
                                Console.ReadLine();
                            }
                            break;
                        case RamProfileFeatures Pfs:
                            var profiles = lol.GetRamProfileFeatures();
                            this.SupportedRamProfiles = GeneralHelpers.ChooseProfileFeatures(profiles);
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
            bool userAnswer = GeneralHelpers.YesOrNoReturnBoolean();
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
