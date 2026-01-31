using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Account
{
    public class AdminAccount : AccountBase
    {
        public string UserName { get; set; }
       
        public void ChangeOwnPassword(string passWord)
        {
            base.SetPassword(passWord);
        }
      

    }
}
