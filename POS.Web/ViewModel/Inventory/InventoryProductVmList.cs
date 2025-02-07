using POS.Domain.Entitties;
using POS.Domain.Entitties.DTO;
using POS.Domain.Entitties.Interfaces;


namespace POS.Web.ViewModel.Inventory
{
    public class InventoryProductVmList
    {
        public InventoryProductDto? InventoryProductDto { get; set; }
        public ProductBrandType? ProductBrandType { get; set; }
    }
}
