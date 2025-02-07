using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ProductBrandVmForIndex : PagingModel
    {
        public IEnumerable<ProductBrand> ProductBrand { get; set; }
    }
}
