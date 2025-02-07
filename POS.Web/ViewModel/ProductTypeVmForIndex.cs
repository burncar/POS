using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ProductTypeVmForIndex : PagingModel
    {
        public IEnumerable<ProductType> ProductType { get; set; }
    }
}
