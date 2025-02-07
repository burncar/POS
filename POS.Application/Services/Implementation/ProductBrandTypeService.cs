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
    public class ProductBrandTypeService : IProductBrandTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductBrandTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Any(int productBrandId = 0, int productTypeId = 0, int id = 0, string? condition = null)
        {
            if (condition != null && id != 0)
            {
                var data = _unitOfWork.ProductBrandType
                    .GetAll(u => u.ProductTypeId == productTypeId && u.ProductBrandId == productBrandId);
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
                return _unitOfWork.ProductBrandType.Any(u => u.ProductTypeId == productTypeId && u.ProductBrandId == productBrandId);
            }
            return false;
        }

        public void CreateProductBrandType(ProductBrandType brandType)
        {
            _unitOfWork.ProductBrandType.Add(brandType);
            _unitOfWork.Save();
        }
       
        public bool DeleteProductBrandType(int id)
        {
            try
            {
                ProductBrandType? objFromDb = _unitOfWork.ProductBrandType.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                   
                    _unitOfWork.ProductBrandType.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProductBrandType> GetAllProductBrandTypes()
        {
            return _unitOfWork.ProductBrandType.GetAll(includeProperties: "ProductBrand, ProductType");
        }

        public ProductBrandType GetProductBrandTypeById(int id)
        {
            return _unitOfWork.ProductBrandType.Get(u => u.Id == id, includeProperties: "ProductBrand, ProductType");
        }

        public void UpdateProductBrandType(ProductBrandType brandType)
        {
            _unitOfWork.ProductBrandType.Update(brandType);
            _unitOfWork.Save();
        }
    }
}
