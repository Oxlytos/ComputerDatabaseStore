using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ComputerStoreApplication.Logic
{
    public class BasicController
    {
        readonly ComputerDBContext _dbContext;

        public BasicController(ComputerDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //APU kallelser här nästan
        public List<ComputerPart?> GetAllProducts()
        {
            //Cast:a alla object som ComputerPart, concat till ett lång mixad lista med objekt av samma basklass
           return  _dbContext.CPUs.Cast<ComputerPart>()
                .Concat(_dbContext.GPUs)
                .Concat(_dbContext.GPUs)
                .Concat(_dbContext.RAMs)
                .Concat (_dbContext.PSUs)
                .Concat(_dbContext.Motherboards).
                ToList();
        }
    }
}
