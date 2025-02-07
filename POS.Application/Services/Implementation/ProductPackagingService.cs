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
    public class ProductPackagingService : IProductPackagingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductPackagingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Any(string packagingType, int id = 0, string? condition = null)
        {
            if (condition != null && id != 0)
            {
                var data = _unitOfWork.ProductPackaging.GetAll(u => u.PackagingType.ToLower().Trim() == packagingType.ToLower().Trim());
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
                return _unitOfWork.ProductPackaging.Any(a => a.PackagingType.ToLower().Trim() == packagingType.ToLower().Trim());
            }
            return false;
        }
        public void CreateProductPackaging(ProductPackaging productPackaging)
        {
            _unitOfWork.ProductPackaging.Add(productPackaging);
            _unitOfWork.Save();
        }

        public bool DeleteProductPackaging(int id)
        {
            try
            {
                ProductPackaging? objFromDb = _unitOfWork.ProductPackaging.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.ProductPackaging.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProductPackaging> GetAllProductPackagings()
        {
            return _unitOfWork.ProductPackaging.GetAll();

        }

        public ProductPackaging GetProductPackagingById(int id)
        {
            return _unitOfWork.ProductPackaging.Get(u => u.Id == id);
        }

        public void UpdateProductPackaging(ProductPackaging productPackaging)
        {
            _unitOfWork.ProductPackaging.Update(productPackaging);
            _unitOfWork.Save();
        }
    }
}
