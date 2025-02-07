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
    public class ProductBrandService : IProductBrandServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductBrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateProductBrand(ProductBrand brand)
        {
            _unitOfWork.ProductBrand.Add(brand);
            _unitOfWork.Save();
        }
        public bool Any(string product, int id = 0, string? condition = null)
        {
            if (condition != null && id != 0)
            {
                var data = _unitOfWork.ProductBrand.GetAll(u => u.BrandName.ToLower().Trim() == product.ToLower().Trim());
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
                return _unitOfWork.ProductBrand.Any(a => a.BrandName.ToLower().Trim() == product.ToLower().Trim());
            }
            return false;
        }
        public bool DeleteProductBrand(int id)
        {
            try
            {
                ProductBrand? objFromDb = _unitOfWork.ProductBrand.Get(u => u.Id == id);
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
                    _unitOfWork.ProductBrand.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProductBrand> GetAllProductBrands()
        {
            return _unitOfWork.ProductBrand.GetAll();
        }

        public ProductBrand GetProductBrandById(int id)
        {
            return _unitOfWork.ProductBrand.Get(u => u.Id == id);
        }

        public void UpdateProductBrand(ProductBrand brand)
        {

            _unitOfWork.ProductBrand.Update(brand);
            _unitOfWork.Save();
        }

     
    }
}
