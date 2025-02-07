using Microsoft.AspNetCore.Mvc;
using POS.Application.Services.Implementation;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Web.ViewModel;

namespace POS.Web.Controllers
{
    public class ProductPackagingController : Controller
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IProductBrandServices _productBrandServices;
        private readonly IProductPackagingService _productPackagingService;
        public ProductPackagingController(IProductPackagingService productPackagingService,IProductTypeService productTypeService, IProductBrandServices productBrandServices)
        {
            _productTypeService = productTypeService;
            _productBrandServices = productBrandServices;
            _productPackagingService = productPackagingService;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            var productPackaging = _productPackagingService.GetAllProductPackagings();
            ProductPackagingVMForIndex productPackagingVMForIndex = new();
            productPackagingVMForIndex.ProductPackaging = productPackaging;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                productPackagingVMForIndex.ProductPackaging = productPackagingVMForIndex.ProductPackaging
                    .Where(a => a.PackagingType.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            productPackagingVMForIndex.ProductPackaging = productPackagingVMForIndex.ProductPackaging.OrderBy(a => a.PackagingType);


            productPackagingVMForIndex.ProductPackaging = productPackagingVMForIndex.ProductPackaging.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

            productPackagingVMForIndex.pageNumber = pageNumber;
            productPackagingVMForIndex.pageSize = pageSize;
            productPackagingVMForIndex.searchString = searchString;
            productPackagingVMForIndex.totalRecords = productPackaging.Count();

            return View(productPackagingVMForIndex);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductPackaging obj)
        {
            if (ModelState.IsValid)
            {
                if (_productPackagingService.Any(obj.PackagingType))
                {
                    return RedirectToAction("Error", "Home");
                }
                _productPackagingService.CreateProductPackaging(obj);
                TempData["success"] = "The Product Packaging Type has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Update(int ProductPackagingId)
        {
            ProductPackaging? obj = _productPackagingService.GetProductPackagingById(ProductPackagingId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
         
            return View(obj);

        }

        [HttpPost]
        public IActionResult Update(ProductPackaging obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                if (_productPackagingService.Any(obj.PackagingType, obj.Id, "Update"))
                {
                    return RedirectToAction("Error", "Home");
                }
                _productPackagingService.UpdateProductPackaging(obj);
                TempData["success"] = "The Product Type has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int ProductPackagingId)
        {
            ProductPackaging? obj = _productPackagingService.GetProductPackagingById(ProductPackagingId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(ProductPackaging obj)
        {
            bool deleted = _productPackagingService.DeleteProductPackaging(obj.Id);
            if (deleted)
            {
                TempData["success"] = "The Product Packaging Type has been deleted successfully.";
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
