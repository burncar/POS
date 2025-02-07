using Microsoft.AspNetCore.Mvc;
using POS.Application.Services.Interfaces;
using POS.Web.ViewModel;

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

    }
}
