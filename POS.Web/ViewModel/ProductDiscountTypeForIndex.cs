using POS.Domain.Entitties;

namespace POS.Web.ViewModel
{
    public class ProductDiscountTypeForIndex :PagingModel
    {
        public IEnumerable<ProductDiscountType> ProductDiscountType { get; set; }
    }
}
