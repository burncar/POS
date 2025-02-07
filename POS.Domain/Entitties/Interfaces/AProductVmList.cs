using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties.Interfaces
{
    public abstract class AProductVmList
    {
        public Product? Product { get; set; }
        public ProductBrand? ProductBrand { get; set; }
        public ProductType? ProductType { get; set; }
    }
}
