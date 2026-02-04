using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Logic
{
    public class ContextFactory
    {
        public ComputerDBContext Create()
        {
            return new ComputerDBContext();
        }
    }
}
