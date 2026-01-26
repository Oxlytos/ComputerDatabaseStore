using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Customer
{
    internal class CustomerCreator
    {
        public static Customer CreateCustomer(Customer currCustomer)
        {
            string firstName = RandomFirstName();
            string surName = RandomSurName();
            Customer customer = new Customer
            {
                FirstName = firstName,
                SurName = surName,
                Email = RandomEmail(firstName, surName),
                PhoneNumber = CustomerCreator.RandomPhoneNumber(),
            };
            return customer;
        }
        internal static string RandomFirstName()
        {
            string[] firstNames =
           {
                // Swed (20)
                "Erik", "Johan", "Anna", "Maria", "Lars", "Karin", "Anders", "Eva", "Joel",
                "Per", "Sara", "Karl", "Emma", "Mats", "Lisa", "Henrik", "Julia",
                "Fredrik", "Sofia", "Daniel",

                // Amer (20)
                "Michael", "Jessica", "John", "Emily", "David", "Ashley", "James", "Hannah",
                "Robert", "Olivia", "William", "Ava", "Matthew", "Chloe", "Christopher", "Grace",
                "Andrew", "Megan", "Joshua", "Abigail",

                // Mid east (20)
                "Ahmed", "Fatima", "Ali", "Layla", "Omar", "Aisha", "Yusuf", "Zara",
                "Hassan", "Noor", "Ibrahim", "Mariam", "Samir", "Leila", "Khalid", "Rania",
                "Tariq", "Salma", "Nadia", "Amir"
            };
            int fNameIndex = Random.Shared.Next(firstNames.Length);
            string firstName = firstNames[fNameIndex];
            return firstName;
        }
        internal static string RandomSurName()
        {
             string[] surNames =
       {
            // Swedi (20)
            "Stormsten", "Eriksson", "Johansson", "Andersson", "Karlsson", "Nilsson",
            "Larsson", "Olsson", "Persson", "Svensson", "Gustafsson", "Pettersson",
            "Lindberg", "Lundqvist", "Bergström", "Lindgren", "Sandberg", "Hansson",
            "Berg", "Holm",

            // American (20)
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Wilson",
            "Taylor", "Anderson", "Thomas", "Moore", "Jackson", "White", "Harris",
            "Martin", "Thompson", "Garcia", "Martinez", "Clark",

            // Mid east (20)
            "Al-Farsi", "Haddad", "Rahman", "Khatib", "Darwish", "Najjar", "Saidi", "Mansour",
            "Aziz", "Saleh", "Nasser", "Abboud", "Tariq", "Hakim", "Farouk", "Zayed",
            "Shadid", "Hosseini", "Bakir", "Al-Mansouri"
        };

            int sNameIndex = Random.Shared.Next(surNames.Length);
            string surName = surNames[sNameIndex];

            return surName;

        }
        internal static string RandomPassword()
        {
            return Guid.NewGuid().ToString();
        }

        internal static string RandomEmail(string fName, string sName)
        {
            int randomNumber = Random.Shared.Next(1, 256);
            string email = $"{fName.ToLower()}.{sName.ToLower()}{randomNumber}@omail.com";

            return email;
        }
        internal static string RandomPhoneNumber()
        {
            string phoneNUmber = "07" + Random.Shared.Next(0, 9).ToString() + "-";
            for (int i = 0; i < 7; i++)
            {
                int rando = Random.Shared.Next(0, 9);
                phoneNUmber += rando.ToString();
            }
            return phoneNUmber;
        }
    }
}
