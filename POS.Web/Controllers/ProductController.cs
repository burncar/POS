using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Application.Services.Implementation;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Web.Models;
using POS.Web.ViewModel;

namespace POS.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductBrandServices _productBrandService;
        private readonly IProductTypeService _productTypeService;
        private readonly IProductBrandTypeService _productBrandTypeService;

        public ProductController(IProductService productService, 
                                 IProductBrandServices productBrandService, 
                                 IProductTypeService productTypeService,
                                 IProductBrandTypeService productBrandTypeService)
        {
            _productService = productService;
            _productBrandService = productBrandService;
            _productTypeService = productTypeService;
            _productBrandTypeService = productBrandTypeService;
        }
        public IActionResult Index(int pageNumber=1, int pageSize=10, string? searchString = null)
        {
            ProductVMforIndex productVMs = new();

            List<ProductVMList> productVMlst = new();
            var products = _productService.GetAllProducts();
                 
            foreach(var product in products)
            {
                ProductVMList productVM = new();
                var productBrand = _productBrandService
                    .GetProductBrandById(product.ProductBrandType.ProductBrandId);
                var productType = _productTypeService
                    .GetProductTypeById(product.ProductBrandType.ProductTypeId);
                productVM.ProductType = productType;
                productVM.ProductBrand = productBrand;
                productVM.Product = product;
                productVMlst.Add(productVM);
            }
            if(!string.IsNullOrWhiteSpace(searchString))
            {
                productVMlst = (List<ProductVMList>)productVMlst
                    .Where(a => a.ProductBrand.BrandName.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                                a.Product.BarCode.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                                a.ProductType.Type.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            productVMs.ProductVMList = productVMlst.OrderBy(a => a.ProductBrand.BrandName)
                                          .ThenBy(a => a.ProductType.Type);
            productVMs.ProductVMList = productVMs.ProductVMList.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

            productVMs.pageNumber = pageNumber;
            productVMs.pageSize = pageSize;
            productVMs.searchString = searchString;
            productVMs.totalRecords = productVMlst.Count();

            return View(productVMs);
        }
        public IActionResult Create()
        {
            ProductVm productVm = new()
            {
                ProductBrandList = _productBrandService.GetAllProductBrands()
                .Select(u => new SelectListItem
                {
                    Text = u.BrandName,
                    Value = u.Id.ToString(),
                    //ProductTypeId = u.ProductTypeId
                }),
                ProductBrandTypeList = _productBrandTypeService.GetAllProductBrandTypes()
                .Select(u => new ExtendedSelectListItem
                {
                    Text = u.ProductType.Type,
                    Value = u.Id.ToString(),
                    ProductBrandId = u.ProductBrandId
                })
            };

           
            return View(productVm);
        }

        [HttpPost]
        public IActionResult Create(ProductVm obj)
        {

            if (ModelState.IsValid)
            {
                if (_productService.Any(obj.Product.BarCode))
                {
                    return RedirectToAction("Error", "Home");
                }

                _productService.CreateProduct(obj.Product);
                TempData["success"] = "The Product has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int ProductId)
        {
            Product? obj = _productService.GetProductById(ProductId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            

            ProductVm productVm = new()
            {
                ProductBrandList = _productBrandService.GetAllProductBrands()
                 .Select(u => new SelectListItem
                 {
                     Text = u.BrandName,
                     Value = u.Id.ToString(),
                     //ProductTypeId = u.ProductTypeId
                 }),
                ProductBrandTypeList = _productBrandTypeService.GetAllProductBrandTypes()
                 .Select(u => new ExtendedSelectListItem
                 {
                     Text = u.ProductType.Type,
                     Value = u.Id.ToString(),
                     ProductTypeId = u.ProductTypeId,
                     ProductBrandId = u.ProductBrandId
                 })
            };

            productVm.Product = obj;


            return View(productVm);

        }
        [HttpPost]
        public IActionResult Update(ProductVm obj)
        {
            if (ModelState.IsValid && obj.Product.Id > 0)
            {
                if (_productService.Any(obj.Product.BarCode, obj.Product.Id, "Update"))
                {
                    return RedirectToAction("Error", "Home");
                }
                _productService.UpdateProduct(obj.Product);
                TempData["success"] = "The Product has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int ProductId)
        {
            Product? obj = _productService.GetProductById(ProductId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Product obj)
        {
            bool deleted = _productService.DeleteProduct(obj.Id);
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
