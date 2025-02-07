using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IProductBrandTypeService
    {
        IEnumerable<ProductBrandType> GetAllProductBrandTypes();
        ProductBrandType GetProductBrandTypeById(int id);
        void CreateProductBrandType(ProductBrandType brandType);
        void UpdateProductBrandType(ProductBrandType brandType);
        bool DeleteProductBrandType(int id);
        bool Any(int productBrandId=0, int productTypeId=0, int id = 0, string? condition = null);
    }
}
