using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Application.Services.Implementation;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Web.ViewModel;

namespace POS.Web.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IProductBrandServices _productBrandServices;
        public ProductTypeController(IProductTypeService productTypeService, IProductBrandServices productBrandServices)
        {
            _productTypeService = productTypeService;
            _productBrandServices = productBrandServices;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            var productType = _productTypeService.GetAllProductTtypes();
            ProductTypeVmForIndex productTypeVmForIndex = new();
            productTypeVmForIndex.ProductType = productType;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                productTypeVmForIndex.ProductType = productTypeVmForIndex.ProductType
                    .Where(a => a.Type.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            productTypeVmForIndex.ProductType = productTypeVmForIndex.ProductType.OrderBy(a => a.Type);


            productTypeVmForIndex.ProductType = productTypeVmForIndex.ProductType.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

            productTypeVmForIndex.pageNumber = pageNumber;
            productTypeVmForIndex.pageSize = pageSize;
            productTypeVmForIndex.searchString = searchString;
            productTypeVmForIndex.totalRecords = productType.Count();

            return View(productTypeVmForIndex);
        }
        public IActionResult Create()
        {
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
            //return View(productTypeVm);
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductType obj)
        {

            if (ModelState.IsValid)
            {
                if (_productTypeService.Any(obj.Type))
                {
                    return RedirectToAction("Error", "Home");
                }

                _productTypeService.CreateProductType(obj);
                TempData["success"] = "The Product Type has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int ProductTypeId)
        {
            ProductType? obj = _productTypeService.GetProductTypeById(ProductTypeId);
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
        public IActionResult Update(ProductType obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                if (_productTypeService.Any(obj.Type, obj.Id, "Update"))
                {
                    return RedirectToAction("Error", "Home");
                }
                _productTypeService.UpdateProductType(obj);
                TempData["success"] = "The Product Type has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int ProductTypeId)
        {
            ProductType? obj = _productTypeService.GetProductTypeById(ProductTypeId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(ProductType obj)
        {
            bool deleted = _productTypeService.DeleteProductType(obj.Id);
            if (deleted)
            {
                TempData["success"] = "The Product Type has been deleted successfully.";
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
