using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Common.Interfaces
{
    public interface IProductRenderedDiscountRepository : IRepository<ProductRenderedDiscount>
    {
        void Update(ProductRenderedDiscount productRenderedDiscount);
    }
}
