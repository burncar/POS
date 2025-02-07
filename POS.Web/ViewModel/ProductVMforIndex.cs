using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ProductVMforIndex : PagingModel
    {
      
        public IEnumerable<ProductVMList>? ProductVMList { get; set; }
   

    }
}
