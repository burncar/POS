using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IProductDiscountTypeService
    {
        IEnumerable<ProductDiscountType> GetAllProductDiscountTypes();
        ProductDiscountType GetProductProductDiscountTypeById(int id);
        void CreateProductDiscountType(ProductDiscountType productDiscountType);
        void UpdateProductDiscountType(ProductDiscountType productDiscountType);
        bool DeleteProductDiscountType(int id);
        bool Any( string DiscountType="", int id = 0, string? condition = null);
    }
}
