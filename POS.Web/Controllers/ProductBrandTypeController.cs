using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Application.Services.Implementation;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Web.ViewModel;

namespace POS.Web.Controllers
{
    public class ProductBrandTypeController : Controller
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IProductBrandServices _productBrandServices;
        private readonly IProductBrandTypeService _productBrandTypeService;
        public ProductBrandTypeController(IProductBrandTypeService productBrandTypeService, IProductTypeService productTypeService, IProductBrandServices productBrandServices)
        {
            _productTypeService = productTypeService;
            _productBrandServices = productBrandServices;
            _productBrandTypeService = productBrandTypeService;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            var productBrandType = _productBrandTypeService.GetAllProductBrandTypes();
            ProductBrandTypeVmForIndex productBrandTypeVm = new();
            productBrandTypeVm.ProductBrandType = productBrandType;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                productBrandTypeVm.ProductBrandType = productBrandTypeVm.ProductBrandType
                    .Where(a => a.ProductBrand.BrandName.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                               
                                a.ProductType.Type.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            productBrandTypeVm.ProductBrandType = productBrandTypeVm.ProductBrandType.OrderBy(a => a.ProductBrand.BrandName)
                                          .ThenBy(a => a.ProductType.Type);
            productBrandTypeVm.ProductBrandType = productBrandTypeVm.ProductBrandType.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

            productBrandTypeVm.pageNumber = pageNumber;
            productBrandTypeVm.pageSize = pageSize;
            productBrandTypeVm.searchString = searchString;
            productBrandTypeVm.totalRecords = productBrandType.Count();

            return View(productBrandTypeVm);
        }
        public IActionResult Create()
        {

            ProductBrandTypeVm productTypeVm = new()
            {
                ProductBrandList = _productBrandServices.GetAllProductBrands()
                .Select(u => new SelectListItem
                {
                    Text = u.BrandName,
                    Value = u.Id.ToString(),
                    //ProductTypeId = u.ProductTypeId
                }),
                ProductTypeList = _productTypeService.GetAllProductTtypes()
                .Select(u => new SelectListItem
                {
                    Text = u.Type,
                    Value = u.Id.ToString(),
                    //ProductTypeId = u.ProductTypeId
                }),

            };
            return View(productTypeVm);
        }

        [HttpPost]
        public IActionResult Create(ProductBrandTypeVm obj)
        {

            if (ModelState.IsValid)
            {
                if (_productBrandTypeService.Any(obj.ProductBrandType.ProductBrandId,obj.ProductBrandType.ProductTypeId))
                {
                    return RedirectToAction("Error", "Home");
                }

                _productBrandTypeService.CreateProductBrandType(obj.ProductBrandType);
                TempData["success"] = "The Product Brand and Type has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int ProductBrandTypeId)
        {
            ProductBrandType? obj = _productBrandTypeService.GetProductBrandTypeById(ProductBrandTypeId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }

            ProductBrandTypeVm productBrandTypeVm = new()
            {
                ProductBrandList = _productBrandServices.GetAllProductBrands()
               .Select(u => new SelectListItem
               {
                   Text = u.BrandName,
                   Value = u.Id.ToString(),
                   //ProductTypeId = u.ProductTypeId
               }),
                ProductTypeList = _productTypeService.GetAllProductTtypes()
               .Select(u => new SelectListItem
               {
                   Text = u.Type,
                   Value = u.Id.ToString(),
                   //ProductTypeId = u.ProductTypeId
               }),

            };

            productBrandTypeVm.ProductBrandType = obj;


            return View(productBrandTypeVm);

        }


        [HttpPost]
        public IActionResult Update(ProductBrandTypeVm obj)
        {
            if (ModelState.IsValid && obj.ProductBrandType.Id > 0)
            {

                if (_productBrandTypeService.Any(obj.ProductBrandType.ProductBrandId, obj.ProductBrandType.ProductTypeId, obj.ProductBrandType.Id, "Update"))
                {
                    return RedirectToAction("Error", "Home");
                }
                _productBrandTypeService.UpdateProductBrandType(obj.ProductBrandType);
                TempData["success"] = "The Product Brand Type has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int ProductBrandTypeId)
        {
            ProductBrandType? obj = _productBrandTypeService.GetProductBrandTypeById(ProductBrandTypeId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(ProductBrandType obj)
        {
            bool deleted = _productBrandTypeService.DeleteProductBrandType(obj.Id);
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
