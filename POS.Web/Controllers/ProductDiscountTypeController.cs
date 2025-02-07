using Microsoft.AspNetCore.Mvc;
using POS.Application.Services.Implementation;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Web.ViewModel;
using POS.Web.ViewModel.Inventory;

namespace POS.Web.Controllers
{
    public class ProductDiscountTypeController : Controller
    {
        private readonly IProductDiscountTypeService _productDiscountTypeService;
        public ProductDiscountTypeController(IProductDiscountTypeService productDiscountTypeService)
        {
            _productDiscountTypeService = productDiscountTypeService;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            ProductDiscountTypeForIndex _productDiscountTypeForIndex = new();
            var vm = _productDiscountTypeService.GetAllProductDiscountTypes();
            _productDiscountTypeForIndex.ProductDiscountType = vm;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                _productDiscountTypeForIndex.ProductDiscountType = _productDiscountTypeForIndex.ProductDiscountType
                    .Where(a => a.DiscountType.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }


                _productDiscountTypeForIndex.ProductDiscountType = _productDiscountTypeForIndex.ProductDiscountType.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

                _productDiscountTypeForIndex.pageNumber = pageNumber;
                _productDiscountTypeForIndex.pageSize = pageSize;
                _productDiscountTypeForIndex.searchString = searchString;
                _productDiscountTypeForIndex.totalRecords = vm.Count();

            return View(_productDiscountTypeForIndex);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductDiscountType obj)
        {

            if (ModelState.IsValid)
            {
                if (_productDiscountTypeService.Any(obj.DiscountType))
                {
                    return RedirectToAction("Error", "Home");
                }

                _productDiscountTypeService.CreateProductDiscountType(obj);
                TempData["success"] = "The Discount Type has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Update(int ProductDiscountTypeId)
        {
            ProductDiscountType? obj = _productDiscountTypeService.GetProductProductDiscountTypeById(ProductDiscountTypeId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            //ProductTypeVm productTypeVm = new()
            //{
            //    ProductBrandList = _productBrandServices.GetAllProductBrands()
            //    .Select(u => new SelectListItem
            //    {
            //        Text = u.BrandName,
            //        Value = u.Id.ToString(),
            //        //ProductTypeId = u.ProductTypeId
            //    }),

            //};
            //productTypeVm.ProductType = obj;
            return View(obj);

        }
        [HttpPost]
        public IActionResult Update(ProductDiscountType obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                if (_productDiscountTypeService.Any(obj.DiscountType, obj.Id, "Update"))
                {
                    return RedirectToAction("Error", "Home");
                }
                _productDiscountTypeService.UpdateProductDiscountType(obj);
                TempData["success"] = "The Product Type has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int ProductDiscountTypeId)
        {
            ProductDiscountType? obj = _productDiscountTypeService.GetProductProductDiscountTypeById(ProductDiscountTypeId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(ProductDiscountType obj)
        {
            bool deleted = _productDiscountTypeService.DeleteProductDiscountType(obj.Id);
            if (deleted)
            {
                TempData["success"] = "The Discount Type has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Failed to delete the villa.";
            }
            return View();
        }


    }
}
