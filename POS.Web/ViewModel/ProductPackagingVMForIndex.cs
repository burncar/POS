using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ProductPackagingVMForIndex : PagingModel
    {
        public IEnumerable<ProductPackaging> ProductPackaging { get; set; }
    }
}
