using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entitties;
using POS.Domain.Entitties.Interfaces;

namespace POS.Web.ViewModel.Inventory
{
    public class InventoryProductVmForIndex : PagingModel
    {
        public IEnumerable<InventoryProductVmList>? InventoryProductVmList { get; set; }
        public List<ProductRendered>? ProductRendered { get; set; } = new List<ProductRendered>();
        public decimal GrandTotal { get; set; }
        public decimal TotalVatable { get; set; }
        public decimal TotalVatableExempt { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal TotalVatWithDiscount { get; set; }
        public decimal TotalVatNoDiscount { get; set; }
        public decimal TotalAmountNetOfVat { get; set; }
        public decimal NetTotalWithDiscount { get; set; }
        public decimal TotalSale { get; set; }
        public bool IsDiscount { get; set; } = false;

        [ValidateNever]
        public IEnumerable<SelectListItem>? ProductDiscountTypeList { get; set; }

        public int ProductDiscountTypeId { get; set; }
        public bool CheckAll { get; set; } = false;

    }
}
