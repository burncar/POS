using POS.Domain.Entitties;

namespace POS.Web.Models.DTO
{
    public class ProductRenderedPrintDto
    {
        public ProductRenderedPrintDto()
        {
            ProductRendereds = new List<ProductRendered>();
        }
        public int ProductRenderedId { get; set; }
        public int TotalBigQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Total { get; set; }
        public List<ProductRendered> ProductRendereds { get; set; }

    }
}
