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
    public class Motherboard : ComputerPart
    {

        public int? CPUSocketId { get; set; }

        public virtual CPUSocket? CPUSocket { get; set; }
        public int? CPUArchitectureId { get; set; }

        public virtual CPUArchitecture? CPUSocketArchitecture { get; set; }
        public int? MemoryTypeId { get; set; }
        public virtual MemoryType? MemoryType { get; set; }
        public bool Overclockable { get; set; }
        public bool Bluetooth { get; set; }
        public bool Wifi { get; set; }
        public bool Soundcard { get; set; }

        public Motherboard() { }

        public override void Create(ApplicationManager lol)
        {
            var vendors = lol.GetVendors();
            var manufacturers = lol.GetManufacturers();
            var memTypes = lol.GetMemoryTypes();
            var sockets = lol.GetCPUSockets();
            var archs = lol.GetCPUArchitectures();

            Console.WriteLine("Name?");
            string name = Console.ReadLine();

            ChipsetVendor vendor = GeneralHelpers.ChooseVendor(vendors);
            Brand manufacturer = GeneralHelpers.ChooseManufacturer(manufacturers);
            MemoryType whatMemoryType = GeneralHelpers.ChooseMemoryType(memTypes);

            CPUSocket socket = GeneralHelpers.ChooseCPUSocket(sockets);
            CPUArchitecture arch = GeneralHelpers.ChooseCPUArch(archs);

            Console.WriteLine("Overclockable?\"");
            bool overclock = GeneralHelpers.YesOrNoReturnBoolean();
            Console.WriteLine("Bluetooth? Type (Y) for yes, (N) for no, then press 'Enter'\"");
            bool bluetooth = GeneralHelpers.YesOrNoReturnBoolean();
            Console.WriteLine("Wifi? Type (Y) for yes, (N) for no, then press 'Enter'\"");
            bool wifi = GeneralHelpers.YesOrNoReturnBoolean();
            Console.WriteLine("Soundcard? Type (Y) for yes, (N) for no, then press 'Enter'\"");
            bool soundCard = GeneralHelpers.YesOrNoReturnBoolean();

            Motherboard motherboard = new Motherboard
            {
                Name=name,
                ChipsetVendor=vendor,
                BrandManufacturer=manufacturer,
                MemoryType=whatMemoryType,
                CPUSocket=socket,
                CPUSocketArchitecture=arch,
                Overclockable=overclock,
                Bluetooth=bluetooth,
                Wifi=wifi,
                Soundcard=soundCard

            };
            lol.SaveNewComponent(motherboard);
        }

        public override void Read(ApplicationManager lol)
        {
            var motherboard = lol.ComputerPartShopDB.AllParts.OfType<Motherboard>().
              Include(ch => ch.MemoryType).
              Include(ch=>ch.ChipsetVendor).
              Include(ch=>ch.CPUSocketArchitecture).
              Include(ch => ch.CPUSocket).
              Include(ch => ch.BrandManufacturer).
              FirstOrDefault(x => x.Id == this.Id);
            if (motherboard == null)
            {
                Console.WriteLine("Some error, returning");
                return;
            }
            //Hämta alla properties
            var propertiers = motherboard.GetType().GetProperties();
            Console.WriteLine($"More technical info {this.Name}");
            foreach (var prop in propertiers)
            {
                //Hämta value på denna property i loopen
                var value = prop.GetValue(motherboard);
                string[] skips = GeneralHelpers.SkippablePropertiesInPrints();
                if (!skips.Contains(prop.Name))
                {
                    //Kolla specifika props, för formatering och att vi får deras properties korrekt
                    switch (value)
                    {
                        case Brand m:
                            Console.WriteLine($"-  {prop.Name} : {m.Name}");
                            break;
                        case ChipsetVendor v:
                            Console.WriteLine($"- {prop.Name} : {v.Name}");
                            break;
                        case CPUArchitecture c:
                            Console.WriteLine($"-  {prop.Name} : {c.Name}");
                            break;
                        case CPUSocket cs:
                            Console.WriteLine($"- {prop.Name} : {cs.Name}");
                            break;
                        case MemoryType c:
                            Console.WriteLine($"{prop.Name} : {c.Name}");
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
                        case CPUArchitecture CA:
                            var archs = lol.GetCPUArchitectures();
                            if (GeneralHelpers.ChangeCPUArch(archs, this))
                            {
                                lol.SaveChangesOnComponent();
                                Console.ReadLine();
                            }
                            break;
                        case CPUSocket SO:
                            var sockets = lol.GetCPUSockets();
                            if (GeneralHelpers.ChangeSocket(sockets, this))
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
