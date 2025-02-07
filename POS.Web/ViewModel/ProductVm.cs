using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entitties;
using POS.Web.Models.DTO;

namespace POS.Web.ViewModel
{
    public class ProductVm
    { 
        public Product? Product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductBrandList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductBrandTypeList { get; set; }


    }
}
