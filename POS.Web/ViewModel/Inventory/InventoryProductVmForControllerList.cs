using POS.Domain.Entitties.DTO;
using POS.Domain.Entitties;

namespace POS.Web.ViewModel.Inventory
{
    public class InventoryProductVmForControllerList
    {
        public InventoryProduct? InventoryProduct { get; set; }
        public ProductBrandType? ProductBrandType { get; set; }
    }
}
