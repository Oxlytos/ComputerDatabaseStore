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
        internal static int LoginCustomer(List<Customer> customers)
        {
                Console.WriteLine("Email?");
                string email = Console.ReadLine();
                var customerWithEmail = customers.FirstOrDefault(x => x.Email == email);
                if (customerWithEmail != null)
                {
                    bool loggedIn = false;
                    while (!loggedIn)
                    {
                        Console.WriteLine("Please provide the correct password for your account");
                        string password = Console.ReadLine();
                        if (password == customerWithEmail.Password)
                        {
                            Console.WriteLine("Logged in!");
                            Console.WriteLine("Welcome " + customerWithEmail.FirstName + " " + customerWithEmail.SurName);
                            Console.ReadLine();
                            loggedIn = true;
                            return customerWithEmail.Id;
                        }
                        else if (password.ToLower() == "q")
                        {
                            //Quit
                            Console.WriteLine("Leaving login screen");
                            Console.ReadLine();
                            return 0;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password, please try again");
                            Console.ReadLine();
                        }
                    }

                }
            else
            {
                Console.WriteLine("No account with that email was found");
                Console.ReadLine();
            }
                //if corrrect
                //LoggedInTrue

                return 0;
            }
        internal static int LogoutCustomer() 
        {
            return 0;
        }
     

    }
}
