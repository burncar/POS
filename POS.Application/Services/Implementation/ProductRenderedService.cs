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
    public class ProductRenderedService : IProductRenderedService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductRenderedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Any(string barCode, int id = 0, string? condition = null)
        {
            if (condition != null && id != 0)
            {
                var data = _unitOfWork.ProductRendered.GetAll(u => u.ProductId == id);
                if (data.Count() > 1)
                {
                    return true;
                }
                else if (data.Count() == 1)
                {
                    var OneData = data.FirstOrDefault();
                    if (OneData.ProductId != id)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return _unitOfWork.ProductRendered.Any(u => u.ProductId == id);
            }
            return false;
        }

        public void CreateProduct(ProductRendered productRendered)
        {
            _unitOfWork.ProductRendered.Add(productRendered);
            _unitOfWork.Save();
        }

        public void CreateProductRenderedMultiple(IEnumerable<ProductRendered> productRendered)
        {
            _unitOfWork.ProductRendered.AddMultiple(productRendered);
            _unitOfWork.Save();
        }

        public bool DeleteProductRendered(int id)
        {
            try
            {
                ProductRendered? objFromDb = _unitOfWork.ProductRendered.Get(u => u.Id == id);
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
                    _unitOfWork.ProductRendered.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProductRendered> GetAllProductRendereds()
        {
            // return _unitOfWork.ProductRendered.GetAll(includeProperties: "ProductPackaging, Product");
            return _unitOfWork.ProductRendered.GetAll();
        }

        public IEnumerable<ProductRendered> GetAllReprintReciept()
        {
            return _unitOfWork.ProductRendered.ReprintReceipt();
        }

        public ProductRendered GetProductRenderedById(int id)
        {
            return _unitOfWork.ProductRendered.Get(u => u.Id == id);
        }

        public IEnumerable<ProductRendered> GetProductRenderedByIdList(int id)
        {
            // return _unitOfWork.ProductRendered.Get(u => u.ProductId == id, includeProperties: "ProductPackaging, Product");
            return _unitOfWork.ProductRendered.GetAll(u => u.ProductRenderedId == id);
        }

        public int GetProductRenderedId()
        {
            return _unitOfWork.ProductRendered.GetLastInsertedId();
        }

        public void UpdateProductRendered(ProductRendered productRendered)
        {
            _unitOfWork.ProductRendered.Update(productRendered);
            _unitOfWork.Save();
        }
    }
}
