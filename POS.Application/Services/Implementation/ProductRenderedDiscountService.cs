using POS.Application.Common.Interfaces;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Implementation
{
    public class ProductRenderedDiscountService : IProductRenderedDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductRenderedDiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateProductRenderedDiscount(ProductRenderedDiscount product)
        {
            _unitOfWork.ProductRenderedDiscount.Add(product);
            _unitOfWork.Save();
        }
    }
}
