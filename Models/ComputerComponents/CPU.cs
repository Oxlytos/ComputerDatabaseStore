using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.Vendors_Producers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public class CPU : ComputerPart
    {
        //https://stackoverflow.com/questions/5542864/how-should-i-declare-foreign-key-relationships-using-code-first-entity-framework

        //https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwith-nrt

        public decimal? MemorySpeedGhz { get; set; }

        public int? Cores { get; set; }

        public int? Threads { get; set; }

        public int? SocketId { get; set; }
        public virtual CPUSocket? SocketType { get; set; }

        public int? CPUArchitectureId { get; set; }
        public virtual CPUArchitecture? CPUArchitecture { get; set; }

        public bool Overclockable { get; set; }

        public decimal? CPUCache { get; set; }

        public CPU() { }

        public override void Create(ApplicationManager lol)
        {
            var vendors = lol.GetVendors();
            var manufacturers = lol.GetManufacturers();
            var sockets = lol.GetCPUSockets();
            var archs = lol.GetCPUArchitectures();

            Console.WriteLine("Name of the CPU?");
            string CPUName = Console.ReadLine();

            ChipsetVendor vendor = GeneralHelpers.ChooseVendor(vendors);
            CPUSocket socket = GeneralHelpers.ChooseCPUSocket(sockets);
            CPUArchitecture arch = GeneralHelpers.ChooseCPUArch(archs);

            Console.WriteLine("Amount of cores?");
            int cores = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("Threadcount?");
            int threads = GeneralHelpers.StringToInt(Console.ReadLine());

            Console.WriteLine("Clock speed (GHz)?");
            decimal clockSpeed = GeneralHelpers.StringToDecimal(Console.ReadLine());

            Console.WriteLine("Overclockable?");
            bool overclockable = GeneralHelpers.YesOrNoReturnBoolean();

            Console.WriteLine("How much CPU cache?");
            decimal cach = GeneralHelpers.StringToDecimal(Console.ReadLine());

            CPU newCPU = new CPU
            {
                Name = CPUName,

                ChipsetVendor = vendor,
                ChipsetVendorId = vendor.Id,

                SocketType = socket,
                SocketId = socket.Id,
                CPUArchitecture = arch,
                CPUArchitectureId = arch.Id,

                Cores = cores,
                Threads = threads,
                MemorySpeedGhz = clockSpeed,
                Overclockable = overclockable,
                CPUCache = cach

            };

            lol.SaveNewComponent(newCPU);
        }

        public override void Read(ApplicationManager lo)
        {
            //Hämta alla properties
            var thisCpu = lo.ComputerPartShopDB.AllParts.OfType<CPU>()
                .Include(ch => ch.ChipsetVendor).
                Include(ch => ch.BrandManufacturer).
                Include(ch => ch.CPUArchitecture)
                .Include(ch => ch.SocketType).
                FirstOrDefault(x=>x.Id==this.Id);
            if(thisCpu == null)
            {
                Console.WriteLine("Error, returning");
                Console.ReadLine();
                return;
            }
            var propertiers = thisCpu.GetType().GetProperties();
            Console.WriteLine($"More technical info {this.Name}");
            foreach (var prop in propertiers)
            {
                //Hämta value på denna property i loopen
                var value = prop.GetValue(thisCpu);
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
                            Console.WriteLine($"-  {prop.Name} : {m.Name}");
                            break;
                        case ChipsetVendor v:
                            Console.WriteLine($"- {prop.Name} : {v.Name}");
                            break;
                        case CPUArchitecture c:
                            Console.WriteLine($"- {prop.Name} : {c.Name}");
                            break;
                        case CPUSocket cs:
                            Console.WriteLine($"- {prop.Name} : {cs.Name}");
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
                            if (GeneralHelpers.ChangeManufacturer(man,this))
                            {
                                lol.SaveChangesOnComponent();
                                Console.ReadLine();
                            }
                            break;
                        case ChipsetVendor V:
                            var vend = lol.GetVendors();
                            if (GeneralHelpers.ChangeVendor(vend,this))
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
            Console.WriteLine($"Do you really want to delete this {this.Name}?");
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
