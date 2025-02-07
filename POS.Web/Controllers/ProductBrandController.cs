
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Application.Services.Implementation;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Web.Models;
using POS.Web.ViewModel;

namespace POS.Web.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly IProductBrandServices _services;
        private readonly IProductTypeService _productTypeService;
        public ProductBrandController(IProductBrandServices services, IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
            _services = services;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            var productBrands = _services.GetAllProductBrands();
            ProductBrandVmForIndex productBrandVmForIndex = new();
            productBrandVmForIndex.ProductBrand = productBrands;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                productBrandVmForIndex.ProductBrand = productBrandVmForIndex.ProductBrand
                    .Where(a => a.BrandName.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            productBrandVmForIndex.ProductBrand = productBrandVmForIndex.ProductBrand.OrderBy(a => a.BrandName);


            productBrandVmForIndex.ProductBrand = productBrandVmForIndex.ProductBrand.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

            productBrandVmForIndex.pageNumber = pageNumber;
            productBrandVmForIndex.pageSize = pageSize;
            productBrandVmForIndex.searchString = searchString;
            productBrandVmForIndex.totalRecords = productBrands.Count();
            
            return View(productBrandVmForIndex);
        }
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductBrand obj)
        {

          
            if (ModelState.IsValid)
            {
                if (_services.Any(obj.BrandName))
                {
                    return RedirectToAction("Error", "Home");
                }

                _services.CreateProductBrand(obj);
                TempData["success"] = "The Product Brand has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int ProductBrandId)
        {

            ProductBrand? obj = _services.GetProductBrandById(ProductBrandId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //ProductBrandVm productBrandVm = new()
            //{
               
            //  ProductTypeList = _productTypeService.GetAllProductTtypes()
              
            //  .Select(u => new SelectListItem
            //  {
            //      Text = u.Type,
            //      Value = u.Id.ToString()
            //  })
            //};
            //productBrandVm.ProductBrand = obj;
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(ProductBrand obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                if (_services.Any(obj.BrandName, obj.Id, "Update"))
                {
                    return RedirectToAction("Error", "Home");
                }

                _services.UpdateProductBrand(obj);
                TempData["success"] = "The Product Brand has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int ProductBrandId)
        {
            ProductBrand? obj = _services.GetProductBrandById(ProductBrandId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }


        [HttpPost]
        public IActionResult Delete(ProductBrand obj)
        {
            bool deleted = _services.DeleteProductBrand(obj.Id);
            if (deleted)
            {
                TempData["success"] = "The Product has been deleted successfully.";
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
