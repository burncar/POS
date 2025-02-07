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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Any(string barCode, int id = 0, string? condition = null)
        {
            if(condition != null && id != 0)
            {
                var data = _unitOfWork.Product.GetAll(u => u.BarCode.ToLower().Trim() == barCode.ToLower().Trim());
                if (data.Count() > 1)
                {
                    return true;
                }
                else if (data.Count() == 1)
                {
                    var OneData = data.FirstOrDefault();
                    if(OneData.Id != id)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return _unitOfWork.Product.Any(a => a.BarCode.ToLower().Trim() == barCode.ToLower().Trim());
            }
            return false;
        }

        public void CreateProduct(Product product)
        {
           
            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                Product? objFromDb = _unitOfWork.Product.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.Product.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            //return _unitOfWork.Product.GetAll(includeProperties: "ProductBrand");
            return _unitOfWork.Product.GetAll(includeProperties: "ProductBrandType");
        }

        public Product GetProductByBarcode(string barcode = "")
        {
            return _unitOfWork.Product.GetProductByBarcode(barcode);
        }

        public Product GetProductById(int id)
        {
            return _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "ProductBrandType");
        }

        public void UpdateProduct(Product product)
        {
          
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
        }


    }
}
