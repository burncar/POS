using POS.Domain.Entitties;
using POS.Web.Models.DTO;

namespace POS.Web.ViewModel.Rendered
{
    public class ProductRenderedPrintVM : PagingModel
    {
        public ProductRenderedPrintVM()
        {
            ProductRenderedPrintDto = new List<ProductRenderedPrintDto>();
        }
        public List<ProductRenderedPrintDto> ProductRenderedPrintDto { get; set; }
    }
}
