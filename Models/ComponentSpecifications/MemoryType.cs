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

namespace ComputerStoreApplication.Models.ComponentSpecifications
{
    public class MemoryType : ComponentSpecification
    {
        //public ICollection<RAM> RAMs { get; set; } = new List<RAM>();
        //public ICollection<Motherboard> Motherboards { get; set;} = new List<Motherboard>();
        //public ICollection<GPU> GPUs { get; set; } = new List<GPU>();

        public override void Create(ApplicationManager lol)
        {
            int maxLength = 12;
            string name = GeneralHelpers.SetName(maxLength);
            MemoryType arch = new MemoryType
            {
                Name = name
            };
            lol.SaveNewSpecification(arch);
        }

        public override void Delete(ApplicationManager lo)
        {
            lo.RemoveComponentSpecifications(this);

        }

        public override void Read(ApplicationManager lol)
        {
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
                    
                        case MemoryType c:
                            Console.WriteLine($"{prop.Name} : {c.Name}");
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
            int maxLength = 8;
            string name = GeneralHelpers.SetName(maxLength);
        }
    }
}
