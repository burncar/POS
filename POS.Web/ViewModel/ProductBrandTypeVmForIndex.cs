using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ProductBrandTypeVmForIndex : PagingModel
    {
        public IEnumerable<ProductBrandType>? ProductBrandType { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductBrandList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductTypeList { get; set; }
    }
}
