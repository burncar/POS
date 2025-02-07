using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ProductBrandTypeVm : PagingModel
    {
        public ProductBrandType? ProductBrandType { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductBrandList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductTypeList { get; set; }
        
    }
}
