using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Application.Services.Implementation;
using POS.Application.Services.Interfaces;
using POS.Application.Services.Strategy;
using POS.Application.Services.Strategy.Context;
using POS.Application.Services.Strategy.Implementation;
using POS.Domain.Entitties;
using POS.Domain.Entitties.Interfaces;
//using POS.Infrastructure.Migrations;
using POS.Web.ViewModel.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace POS.Web.Controllers
{
    public class HomeController : Controller
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

        public HomeController(IProductService productService,
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
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            InventoryProductVmForIndex inventoryProductVmForIndex = new();

            List<InventoryProductVmList> inventoryProductVmList = new();

            var productsInvs = _inventoryProductService.GetAllExistingInventoryQuantity();

            foreach (var productsInv in productsInvs)
            {
               
                InventoryProductVmList inventoryProductVm = new();
                inventoryProductVm.InventoryProductDto = productsInv;
                var productBrandType =
                    _productBrandTypeService.GetProductBrandTypeById((int)productsInv.Product.ProductBrandTypeId);

                inventoryProductVm.ProductBrandType = productBrandType;

                inventoryProductVmList.Add(inventoryProductVm);
            }


            if (!string.IsNullOrWhiteSpace(searchString))
            {
                inventoryProductVmList = (List<InventoryProductVmList>)inventoryProductVmList
                    .Where(a => a.ProductBrandType.ProductBrand.BrandName.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                                a.InventoryProductDto.Product.BarCode.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                                a.ProductBrandType.ProductType.Type.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }

            inventoryProductVmForIndex.InventoryProductVmList = inventoryProductVmList.OrderBy(a => a.ProductBrandType.ProductBrand.BrandName)
                                          .ThenBy(a => a.ProductBrandType.ProductType.Type);
            inventoryProductVmForIndex.InventoryProductVmList = inventoryProductVmForIndex.InventoryProductVmList.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

            inventoryProductVmForIndex.pageNumber = pageNumber;
            inventoryProductVmForIndex.pageSize = pageSize;
            inventoryProductVmForIndex.searchString = searchString;
            inventoryProductVmForIndex.totalRecords = productsInvs.Count();

            inventoryProductVmForIndex.ProductDiscountTypeList = _productDiscountTypeService.GetAllProductDiscountTypes()
               .Select(u => new SelectListItem
               {
                   Text = u.DiscountType,
                   Value = u.Id.ToString(),
                   //ProductTypeId = u.ProductTypeId
               });

            return View(inventoryProductVmForIndex);
        }

        [HttpGet]
        public IActionResult PartialIndex(int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            InventoryProductVmForIndex inventoryProductVmForIndex = new();

            List<InventoryProductVmList> inventoryProductVmList = new();

            var productsInvs = _inventoryProductService.GetAllExistingInventoryQuantity();

            foreach (var productsInv in productsInvs)
            {
                InventoryProductVmList inventoryProductVm = new();
                inventoryProductVm.InventoryProductDto = productsInv;
                var productBrandType =
                    _productBrandTypeService.GetProductBrandTypeById((int)productsInv.Product.ProductBrandTypeId);

                //var productBrand = _productBrandService
                //    .GetProductBrandById(product.Product.ProductBrandId);
                //var productType = _productTypeService
                //    .GetProductTypeById(product.ProductBrandType.ProductTypeId);
                //inventoryProductVm.ProductType = productType;
                //inventoryProductVm.ProductBrand = productBrand;
                inventoryProductVm.ProductBrandType = productBrandType;

                inventoryProductVmList.Add(inventoryProductVm);
            }


            if (!string.IsNullOrWhiteSpace(searchString))
            {
                inventoryProductVmList = (List<InventoryProductVmList>)inventoryProductVmList
                    .Where(a => a.ProductBrandType.ProductBrand.BrandName.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                                a.InventoryProductDto.Product.BarCode.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                                a.ProductBrandType.ProductType.Type.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }

            inventoryProductVmForIndex.InventoryProductVmList = inventoryProductVmList.OrderBy(a => a.ProductBrandType.ProductBrand.BrandName)
                                          .ThenBy(a => a.ProductBrandType.ProductType.Type);
            inventoryProductVmForIndex.InventoryProductVmList = inventoryProductVmForIndex.InventoryProductVmList.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize);

            inventoryProductVmForIndex.pageNumber = pageNumber;
            inventoryProductVmForIndex.pageSize = pageSize;
            inventoryProductVmForIndex.searchString = searchString;
            inventoryProductVmForIndex.totalRecords = productsInvs.Count();

            return PartialView("_RenderedTable",inventoryProductVmForIndex);
        }

        [HttpPost]
        public IActionResult GetItemList([FromBody]InventoryProductVmForIndex _prodRendereds)
        {

        
            var GrandTotal = _prodRendereds.ProductRendered.Sum(a => a.TotalItemPrice);
            decimal uGrandTotal = GrandTotal; 
            if (_prodRendereds.IsDiscount)
            {
                _prodRendereds.IsDiscount = true;
                var getDataToBeDiscounted = _prodRendereds.ProductRendered.Where(a => a.IsChecked == true && a.Vatable == true);
                var getDataVatable = _prodRendereds.ProductRendered.Where(a => a.IsChecked == false && a.Vatable == true);
                if (getDataToBeDiscounted.Any() && _prodRendereds.ProductDiscountTypeId > 0)
                {
                    
                    decimal uTotalDiscount = 0;
                    
                    foreach (var item in getDataToBeDiscounted) 
                    {
                        var discountDetails = _productDiscountTypeService.GetProductProductDiscountTypeById(_prodRendereds.ProductDiscountTypeId);
                        GetDiscountAndVat withDiscount = GetDiscountAndVat.GetInstance
                            (discountDetails.DiscountIdentity, discountDetails.DiscountRate, item.TotalItemPrice);
                        var createdDiscount = withDiscount.ComputeDiscount();

                        _prodRendereds.TotalAmountNetOfVat = _prodRendereds.TotalAmountNetOfVat + withDiscount.ComputeAmoutLessVat();
                        _prodRendereds.TotalVatWithDiscount = _prodRendereds.TotalVatWithDiscount + withDiscount.GetVat();

                        uTotalDiscount = uTotalDiscount + createdDiscount;
                        item.Discount = createdDiscount;
                      
                    }
                    _prodRendereds.CheckAll = getDataToBeDiscounted.Count() == _prodRendereds.ProductRendered.Count();
                    
                    uGrandTotal = uGrandTotal - uTotalDiscount;
                    _prodRendereds.TotalDiscounts = uTotalDiscount;
                    _prodRendereds.TotalVatableExempt += _prodRendereds.TotalAmountNetOfVat;
                }
              

                if (getDataVatable.Any())
                {
                    _prodRendereds.TotalVatable = getDataVatable.Sum(a => a.TotalItemPrice);
                    GetDiscountAndVat noDiscount = GetDiscountAndVat.GetInstance
                           ("", 0, _prodRendereds.TotalVatable);
                    _prodRendereds.TotalVatable = noDiscount.ComputeAmoutLessVat();
                    _prodRendereds.TotalVatNoDiscount += noDiscount.GetVat();
                }

            }
            else
            {
                var getDataVatable = _prodRendereds.ProductRendered.Where(a => a.IsChecked == false && a.Vatable == true);

                if (getDataVatable.Any())
                {
                    _prodRendereds.TotalVatable = getDataVatable.Sum(a => a.TotalItemPrice);
                    GetDiscountAndVat noDiscount = GetDiscountAndVat.GetInstance
                           ("", 0, _prodRendereds.TotalVatable);
                    _prodRendereds.TotalVatable = noDiscount.ComputeAmoutLessVat();
                    _prodRendereds.TotalVatNoDiscount += noDiscount.GetVat();
                }

              
                _prodRendereds.IsDiscount = false;
                _prodRendereds.ProductDiscountTypeId = 0;

            }

           
       

            _prodRendereds.NetTotalWithDiscount = uGrandTotal;
            _prodRendereds.GrandTotal = GrandTotal;

            return PartialView("_itemList", _prodRendereds);
        }
        [HttpPost]
        public IActionResult ClearList([FromBody] InventoryProductVmForIndex _prodRendereds)
        {
            _prodRendereds.ProductRendered = new List<ProductRendered>();
            return PartialView("_itemList", _prodRendereds);
        }
    
        [HttpPost]
        public IActionResult Create([FromBody] InventoryProductVmForIndex _prodRendereds)
        {
            var productRenderedList = _prodRendereds.ProductRendered.ToList();
            int getId = _productRenderedService.GetProductRenderedId();
            productRenderedList.ForEach(p => p.ProductRenderedId = getId);


            _productRenderedService.CreateProductRenderedMultiple(productRenderedList);


            _prodRendereds.ProductRendered = new List<ProductRendered>();

            var getTotalDiscounts = productRenderedList.Sum(a => a.Discount);
            if (getTotalDiscounts > 0)
            {
                ProductRenderedDiscount prodRenederedDiscount = new();
                prodRenederedDiscount.DiscountTotal = getTotalDiscounts;
                prodRenederedDiscount.ProductDiscountTypeId = _prodRendereds.ProductDiscountTypeId;
                prodRenederedDiscount.ProductRenderedId = getId;

                _productRenderedDiscountService.CreateProductRenderedDiscount(prodRenederedDiscount);
            }

            return PartialView("_itemList", _prodRendereds);
        }

        [HttpPost]
        public IActionResult CreateIndexRefresh([FromBody] InventoryProductVmForIndex _prodRendereds)
        {
            var productsInvs = _inventoryProductService.GetAllExistingInventoryQuantity();
            List<InventoryProductVmList> inventoryProductVmList = new();
           
            foreach (var productsInv in productsInvs)
            {

                InventoryProductVmList inventoryProductVm = new();
                inventoryProductVm.InventoryProductDto = productsInv;
                var productBrandType =
                    _productBrandTypeService.GetProductBrandTypeById((int)productsInv.Product.ProductBrandTypeId);

                inventoryProductVm.ProductBrandType = productBrandType;

                inventoryProductVmList.Add(inventoryProductVm);
            }
            _prodRendereds.InventoryProductVmList = inventoryProductVmList;


            _prodRendereds.InventoryProductVmList = inventoryProductVmList.OrderBy(a => a.ProductBrandType.ProductBrand.BrandName)
                                          .ThenBy(a => a.ProductBrandType.ProductType.Type);

            return PartialView("_RenderedTable", _prodRendereds);
        }





        }
}
