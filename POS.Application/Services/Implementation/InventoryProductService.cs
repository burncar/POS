using POS.Application.Common.Interfaces;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Domain.Entitties.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Implementation
{
    public class InventoryProductService : IInventoryProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InventoryProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateInventoryProduct(InventoryProduct inventoryProduct)
        {
            _unitOfWork.InventoryProduct.Add(inventoryProduct);
            _unitOfWork.Save();
        }

        public bool DeleteInventoryProduct(int id)
        {
            try
            {
                InventoryProduct? objFromDb = _unitOfWork.InventoryProduct.Get(u => u.Id == id);
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
                    _unitOfWork.InventoryProduct.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<InventoryProductDto> GetAllExistingInventoryQuantity()
        {
            return _unitOfWork.InventoryProduct.GetAllExistingQuantity();
        }

        public IEnumerable<InventoryProduct> GetAllInventoryProducts()
        {
            return _unitOfWork.InventoryProduct.GetAll(includeProperties: "ProductPackaging, Product");
        }

        public InventoryProduct GetInventoryProductById(int id)
        {
            return _unitOfWork.InventoryProduct.Get(u => u.Id == id, includeProperties: "ProductPackaging, Product");
        }

        public void UpdateInventoryProduct(InventoryProduct inventoryProduct)
        {
            _unitOfWork.InventoryProduct.Update(inventoryProduct);
            _unitOfWork.Save();
        }
    }
}
