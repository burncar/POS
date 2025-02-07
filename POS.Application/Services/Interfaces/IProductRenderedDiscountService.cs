using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IProductRenderedDiscountService
    {
        //IEnumerable<ProductRenderedDiscount> GetAllProductRenderedDiscounts();
       // ProductRenderedDiscount GetProductRenderedDiscountById(int id);
        void CreateProductRenderedDiscount(ProductRenderedDiscount product);
       // void UpdateProductRenderedDiscount(ProductRenderedDiscount product);
      //  bool DeleteProductRenderedDiscount(int id);
        
    }
}
