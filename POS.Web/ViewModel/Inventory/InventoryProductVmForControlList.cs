using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using POS.Domain.Entitties;

namespace POS.Web.ViewModel.Inventory
{
    public class InventoryProductVmForControlList : PagingModel
    {
        public IEnumerable<InventoryProductVmForControllerList>? InventoryProductVmForControllerList { get; set; }
        public List<ProductRendered>? ProductRendered { get; set; } = new List<ProductRendered>();
    
   
    }
}
