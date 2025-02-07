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
    public class ProductDiscountTypeService : IProductDiscountTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductDiscountTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Any(string DiscountType = "", int id = 0, string? condition = null)
        {
            if (condition != null && id != 0)
            {
                var data = _unitOfWork.ProductDiscountType
                    .GetAll(u => u.DiscountType.ToLower().Trim() == DiscountType.ToLower().Trim());
                if (data.Count() > 1)
                {
                    return true;
                }
                else if (data.Count() == 1)
                {
                    var OneData = data.FirstOrDefault();
                    if (OneData.Id != id)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return _unitOfWork.ProductDiscountType.Any(u => u.DiscountType.ToLower().Trim() == DiscountType.ToLower().Trim() && u.Id == id);
            }
            return false;
        }

        public void CreateProductDiscountType(ProductDiscountType productDiscountType)
        {
            _unitOfWork.ProductDiscountType.Add(productDiscountType);
            _unitOfWork.Save();
        }

        public bool DeleteProductDiscountType(int id)
        {
            try
            {
                ProductDiscountType? objFromDb = _unitOfWork.ProductDiscountType.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.ProductDiscountType.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProductDiscountType> GetAllProductDiscountTypes()
        {
         return _unitOfWork.ProductDiscountType.GetAll();
        }

        public ProductDiscountType GetProductProductDiscountTypeById(int id)
        {
            return _unitOfWork.ProductDiscountType.Get(u => u.Id == id);
        }

        public void UpdateProductDiscountType(ProductDiscountType productDiscountType)
        {
            _unitOfWork.ProductDiscountType.Update(productDiscountType);
            _unitOfWork.Save();
        }
    }
}
