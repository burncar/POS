using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entitties;
using POS.Web.Models;

namespace POS.Web.ViewModel.Inventory
{
    public class InventoryProductVm
    {
        public InventoryProduct InventoryProduct { get; set; }

        [ValidateNever]
        public IEnumerable<EProductListSelectListItem>? ProductList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductPackagingList { get; set; }

        //[ValidateNever]
        //public IEnumerable<SelectListItem>? ProductBrandList { get; set; }
        //[ValidateNever]
        //public IEnumerable<SelectListItem>? ProductBrandTypeList { get; set; }
    }
}
