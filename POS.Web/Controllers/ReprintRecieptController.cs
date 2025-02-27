﻿using Microsoft.AspNetCore.Mvc;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Web.ViewModel;
using POS.Web.ViewModel.Rendered;

namespace POS.Web.Controllers
{
    public class ReprintRecieptController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductBrandServices _productBrandService;
        private readonly IProductTypeService _productTypeService;
        private readonly IProductBrandTypeService _productBrandTypeService;
        private readonly IInventoryProductService _inventoryProductService;
        private readonly IProductPackagingService _productPackagingService;
        private readonly IProductRenderedService _productRenderedService;
        private readonly IProductDiscountTypeService _productDiscountTypeService;
        private readonly IProductRenderedDiscountService _productRenderedDiscountService;

        public ReprintRecieptController(IProductService productService,
                                 IProductBrandServices productBrandService,
                                 IProductTypeService productTypeService,
                                 IProductBrandTypeService productBrandTypeService,
                                 IInventoryProductService inventoryProductService,
                                 IProductPackagingService productPackagingService,
                                 IProductRenderedService productRenderedService,
                                 IProductDiscountTypeService productDiscountTypeService,
                                 IProductRenderedDiscountService productRenderedDiscountService)
        {
            _productService = productService;
            _productBrandService = productBrandService;
            _productTypeService = productTypeService;
            _productBrandTypeService = productBrandTypeService;
            _inventoryProductService = inventoryProductService;
            _productPackagingService = productPackagingService;
            _productRenderedService = productRenderedService;
            _productDiscountTypeService = productDiscountTypeService;
            _productRenderedDiscountService = productRenderedDiscountService;
        }
        public IActionResult Index()
        {
            var data = _productRenderedService.GetAllReprintReciept();
            ReprintRecieptVmForIndex dataIndex = new ReprintRecieptVmForIndex();
            dataIndex.ProductRendered = data;
            return View(dataIndex);
        }

        public IActionResult Update(int ReprintRecieptId)
        {
            if(ReprintRecieptId == 0)
            {
                return NotFound();
            }
            var data = _productRenderedService.GetProductRenderedByIdList(ReprintRecieptId);

            var jsonData = new ProductRenderedVM
            {
                Id = data.FirstOrDefault().Id,
                ProductRendered = (List<Domain.Entitties.ProductRendered>)data

            };

            return View(jsonData);
        }

        [HttpPost]
        public IActionResult Update(int ReprintRecieptId, ProductRenderedVM vm)
        {
            if (ReprintRecieptId == 0)
            {
                return NotFound();
            }
            //var data = _productRenderedService.GetProductRenderedByIdList(ReprintRecieptId);

            foreach(var v in vm.ProductRendered)
            {
                if(v.BigQuantity > 0)
                {
                    _productRenderedService.UpdateProductRendered(v);
                }
               
            }
            TempData["success"] = "The Product has been Updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int ReprintRecieptId)
        {
            ProductRendered? obj = _productRenderedService.GetProductRenderedById(ReprintRecieptId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(ProductRendered obj)
        {
            bool deleted = _productRenderedService.DeleteProductRendered(obj.Id);
            if (deleted)
            {
                TempData["success"] = "The Product has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Failed to delete the rendered product.";
            }
            return View();
        }

    }
}
