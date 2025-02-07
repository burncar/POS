using POS.Application.Common.Interfaces;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Implementation
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        public void CreateProductType(ProductType product)
        {
            _unitOfWork.ProductType.Add(product);
            _unitOfWork.Save();
        }

        public bool Any(string product, int id = 0, string? condition = null)
        {
            if (condition != null && id != 0)
            {
                var data = _unitOfWork.ProductType.GetAll(u => u.Type.ToLower().Trim() == product.ToLower().Trim());
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
                return _unitOfWork.ProductType.Any(a => a.Type.ToLower().Trim() == product.ToLower().Trim());
            }
            return false;
        }
        public bool DeleteProductType(int id)
        {
            try
            {
                ProductType? objFromDb = _unitOfWork.ProductType.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                    //if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
                    //{
                    //    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.ImageUrl.TrimStart('\\'));

                    //    if (System.IO.File.Exists(oldImagePath))
                    //    {
                    //        System.IO.File.Delete(oldImagePath);
                    //    }
                    //}
                    _unitOfWork.ProductType.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProductType> GetAllProductTtypes()
        {
            return _unitOfWork.ProductType.GetAll();
        }

        public ProductType GetProductTypeById(int id)
        {
            return _unitOfWork.ProductType.Get(u => u.Id == id);
        }

        public void UpdateProductType(ProductType product)
        {
            _unitOfWork.ProductType.Update(product);
            _unitOfWork.Save();
        }


    }
}
