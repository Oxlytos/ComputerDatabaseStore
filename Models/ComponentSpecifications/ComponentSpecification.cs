using ComputerStoreApplication.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.ComponentSpecifications
{
    public abstract class ComponentSpecification
    {
        public int Id { get; set; }
        [StringLength(60)]
        public virtual string Name { get; set; } = string.Empty;

        public abstract void Create(ApplicationManager lol);
        public abstract void Read(ApplicationManager lol);
        public abstract void Update(ApplicationManager lol);
        public abstract void Delete(ApplicationManager lo);

    }
}
