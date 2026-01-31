using ComputerStoreApplication.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComputerComponents
{
    public class ComponentCategory
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ComputerPart> ComputerParts { get; set; }

        public ComponentCategory Create()
        {
            Console.WriteLine("Name of this cateogory?");
            Name = GeneralHelpers.SetName(100);

            return this;
        }
        public void Read()
        {

        }
        public ComponentCategory Update()
        {


            return this;
        }
    }
}
