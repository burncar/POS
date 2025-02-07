using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ReprintRecieptVmForIndex : PagingModel
    {
        [ValidateNever]
        public IEnumerable<ProductRendered>? ProductRendered { get; set; }
      
    }
}
