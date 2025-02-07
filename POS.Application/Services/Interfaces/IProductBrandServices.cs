using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IProductBrandServices
    {
        IEnumerable<ProductBrand> GetAllProductBrands();
        ProductBrand GetProductBrandById(int id);
        void CreateProductBrand(ProductBrand brand);
        void UpdateProductBrand(ProductBrand brand);
        bool DeleteProductBrand(int id);
        bool Any(string product, int id = 0, string? condition = null);

    }
}
