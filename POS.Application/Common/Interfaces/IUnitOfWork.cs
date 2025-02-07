using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IProductBrandRepository ProductBrand { get; }
        IProductRepository Product { get; }
        IProductTypeRepository ProductType { get; }
        IProductBrandTypeRepository ProductBrandType { get; }   
        IProductPackagingRepository ProductPackaging { get; }
        IInventoryProductRepository InventoryProduct { get; }
        IProductRenderedRepository ProductRendered { get; }
        IProductDiscountTypeRepository ProductDiscountType { get; }
        IProductRenderedDiscountRepository ProductRenderedDiscount { get; }
        void Save();
    }
}
