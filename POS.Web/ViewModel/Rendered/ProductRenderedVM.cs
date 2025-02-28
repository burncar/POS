using POS.Domain.Entitties;

namespace POS.Web.ViewModel.Rendered
{
    public class ProductRenderedVM
    {
        public ProductRenderedVM()
        {
            ProductRendered = new List<ProductRendered>();
        }
        public int Id { get; set; }
        public List<ProductRendered> ProductRendered { get; set; }
    }
}
