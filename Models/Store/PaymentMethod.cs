using ComputerStoreApplication.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }

        public PaymentMethod Create()
        {
            Console.WriteLine("Input name of the service");
            Name = GeneralHelpers.SetName(30);
            return this;
        }
        public  void Read()
        {
            //Print data about how many customers use this, and average spent
        }
        public PaymentMethod Update()
        {
            Console.WriteLine("Change Name of provider?");
            bool yes = GeneralHelpers.YesOrNoReturnBoolean();
            if (yes)
            {
                Name = GeneralHelpers.SetName(20);
            }
            return this;
        }
    }
}
