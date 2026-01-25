using ComputerStoreApplication.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Customer
{
    public class AccountLogic
    {

        public static Customer LoginCustomer(ApplicationManager applol)
        {
            
            var customers = applol.GetCustomers();
            foreach (var customer in customers) 
            {
            
            }
            Console.WriteLine("Email?");
            Console.WriteLine("Password?");
            applol.IsLoggedInAsCustomer = true;
            //if corrrect
            //LoggedInTrue

            return null;
        }
    }
}
