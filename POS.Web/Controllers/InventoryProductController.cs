using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Application.Common.Interfaces;
using POS.Application.Services.Interfaces;
using POS.Domain.Entitties;
using POS.Web.Models;
using POS.Web.ViewModel;
using POS.Web.ViewModel.Inventory;

namespace POS.Web.Controllers
{
    public class InventoryProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductBrandServices _productBrandService;
        private readonly IProductTypeService _productTypeService;
        private readonly IProductBrandTypeService _productBrandTypeService;
        private readonly IInventoryProductService _inventoryProductService;
        private readonly IProductPackagingService _productPackagingService;

        public InventoryProductController(IProductService productService,
                                 IProductBrandServices productBrandService,
                                 IProductTypeService productTypeService,
                                 IProductBrandTypeService productBrandTypeService,
                                 IInventoryProductService inventoryProductService,
                                 IProductPackagingService productPackagingService)
        {
            _productService = productService;
            _productBrandService = productBrandService;
            _productTypeService = productTypeService;
            _productBrandTypeService = productBrandTypeService;
            _inventoryProductService = inventoryProductService;
            _productPackagingService = productPackagingService;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            InventoryProductVmForControlList inventoryProductVmForControlList = new();

            List<InventoryProductVmForControllerList> inventoryProductVmForControllerList = new();
            var productsInvs = _inventoryProductService.GetAllInventoryProducts();

            foreach (var productsInv in productsInvs)
            {
                InventoryProductVmForControllerList inventoryProductVm = new();
                inventoryProductVm.InventoryProduct = productsInv;
                var productBrandType = 
                    _productBrandTypeService.GetProductBrandTypeById((int)productsInv.Product.ProductBrandTypeId);

                //var productBrand = _productBrandService
                //    .GetProductBrandById(product.Product.ProductBrandId);
                //var productType = _productTypeService
                //    .GetProductTypeById(product.ProductBrandType.ProductTypeId);
                //inventoryProductVm.ProductType = productType;
                //inventoryProductVm.ProductBrand = productBrand;
                inventoryProductVm.ProductBrandType = productBrandType;

                inventoryProductVmForControllerList.Add(inventoryProductVm);
            }


            if (!string.IsNullOrWhiteSpace(searchString))
            {
                inventoryProductVmForControllerList = (List<InventoryProductVmForControllerList>)inventoryProductVmForControllerList
                    .Where(a => a.ProductBrandType.ProductBrand.BrandName.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                                a.InventoryProduct.Product.BarCode.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                                a.ProductBrandType.ProductType.Type.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }

            inventoryProductVmForControlList.InventoryProductVmForControllerList = inventoryProductVmForControllerList.OrderBy(a => a.ProductBrandType.ProductBrand.BrandName)
                                          .ThenBy(a => a.ProductBrandType.ProductType.Type);
            inventoryProductVmForControlList.InventoryProductVmForControllerList = inventoryProductVmForControllerList.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

            inventoryProductVmForControlList.pageNumber = pageNumber;
            inventoryProductVmForControlList.pageSize = pageSize;
            inventoryProductVmForControlList.searchString = searchString;
            inventoryProductVmForControlList.totalRecords = productsInvs.Count();

            return View(inventoryProductVmForControlList);
        }

        public IActionResult Create()
        {
           
            InventoryProductVm inventoryProductVm = new()
            {

                ProductList = _productService.GetAllProducts()
                .Select(u => new EProductListSelectListItem
                {
                    Text = u.ProductDescription,
                    Value = u.Id.ToString(),
                    ProductBrandId = (int)u.ProductBrandType.ProductBrandId,
                    ProductTypeId = (int)u.ProductBrandType.ProductTypeId,
                    ProductBrandName = _productBrandService
                                       .GetProductBrandById((int)u.ProductBrandType.ProductBrandId)
                                       .BrandName.ToString(),
                    ProductTypeName = _productTypeService.GetProductTypeById((int)u.ProductBrandType.ProductTypeId)
                    .Type.ToString(),
                }),
                ProductPackagingList = _productPackagingService.GetAllProductPackagings()
                .Select(u => new SelectListItem{
                    Text = u.PackagingType+" - "+u.ItemPerPiece,
                    Value = u.Id.ToString(),
                })
            };
            return View(inventoryProductVm);
        }

        [HttpPost]
        public IActionResult Create(InventoryProductVm obj)
        {

            if (ModelState.IsValid)
            {
               
                _inventoryProductService.CreateInventoryProduct(obj.InventoryProduct);
                TempData["success"] = "The Product has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int InventoryProductId)
        {
            InventoryProduct? obj = _inventoryProductService.GetInventoryProductById(InventoryProductId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            InventoryProductVm inventoryProductVm = new()
            {
                ProductList = _productService.GetAllProducts()
                 .Select(u => new EProductListSelectListItem
                 {
                     Text = u.ProductDescription,
                     Value = u.Id.ToString(),
                     ProductBrandName = _productBrandService.GetProductBrandById(u.ProductBrandType.ProductBrandId).BrandName,
                     ProductBrandId = u.ProductBrandType.ProductBrandId,
                     ProductTypeName = _productTypeService.GetProductTypeById(u.ProductBrandType.ProductTypeId).Type,
                     ProductTypeId = u.ProductBrandType.ProductTypeId
                 }),
                ProductPackagingList = _productPackagingService.GetAllProductPackagings()
                 .Select(u => new SelectListItem
                 {
                     Text = u.PackagingType+" - "+u.ItemPerPiece,
                     Value = u.Id.ToString(),
                   
                 })
            };

            inventoryProductVm.InventoryProduct = obj;


            return View(inventoryProductVm);

        }
        [HttpPost]
        public IActionResult Update(InventoryProductVm obj)
        {
            if (ModelState.IsValid && obj.InventoryProduct.Id > 0)
            {
                
                _inventoryProductService.UpdateInventoryProduct(obj.InventoryProduct);
                TempData["success"] = "The Product has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int InventoryProductId)
        {
            InventoryProduct? obj = _inventoryProductService.GetInventoryProductById(InventoryProductId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(InventoryProduct obj)
        {
            bool deleted = _inventoryProductService.DeleteInventoryProduct(obj.Id);
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
