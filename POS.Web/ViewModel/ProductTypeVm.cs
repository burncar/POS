using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ProductTypeVm
    {
        public ProductType? ProductType { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductBrandList { get; set; }
    }
}
