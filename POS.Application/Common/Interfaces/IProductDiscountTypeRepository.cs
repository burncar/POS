using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Common.Interfaces
{
    public interface IProductDiscountTypeRepository: IRepository<ProductDiscountType>
    {
        void Update(ProductDiscountType productDiscountType);
    }
}
