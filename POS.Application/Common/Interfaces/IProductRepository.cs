using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Common.Interfaces
{
    public interface IProductRepository :IRepository<Product>
    {
        void Update(Product brand);
        Product GetProductByBarcode(string barcode = "");
    }
}
