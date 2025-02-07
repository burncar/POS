using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IProductPackagingService
    {
        bool Any(string packagingType, int id = 0, string? condition = null);
        IEnumerable<ProductPackaging> GetAllProductPackagings();
        ProductPackaging GetProductPackagingById(int id);
        void CreateProductPackaging(ProductPackaging productPackaging);
        void UpdateProductPackaging(ProductPackaging productPackaging);
        bool DeleteProductPackaging(int id);
    }
}
