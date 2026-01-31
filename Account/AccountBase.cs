using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Account
{
    public abstract class AccountBase
    {
        protected AccountBase() { }
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        //Sällan längre
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string SurName { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string PhoneNumber { get; set; }

        public string Password { get; private set; }

        public void CreatePassword()
        {
            if (Password.IsNullOrEmpty())
            {
                Password = Guid.NewGuid().ToString();
            }
        }

        protected void SetPassword(string password)
        {
            Password = password;
        }
        
    }
}
