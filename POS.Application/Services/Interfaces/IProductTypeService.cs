using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IProductTypeService
    {
        IEnumerable<ProductType> GetAllProductTtypes();
        ProductType GetProductTypeById(int id);
        void CreateProductType(ProductType product);
        void UpdateProductType(ProductType product);
        bool DeleteProductType(int id);
        bool Any(string product, int id = 0, string? condition = null);
    }
}
