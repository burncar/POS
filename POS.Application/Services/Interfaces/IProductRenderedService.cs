using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IProductRenderedService
    {
        IEnumerable<ProductRendered> GetAllProductRendereds();
        IEnumerable<ProductRendered> GetAllReprintReciept();
        ProductRendered GetProductRenderedById(int id);
        IEnumerable<ProductRendered> GetProductRenderedByIdList(int id);
        void CreateProduct(ProductRendered productRendered);

        void CreateProductRenderedMultiple(IEnumerable<ProductRendered> productRendered);
        void UpdateProductRendered(ProductRendered productRendered);
        bool DeleteProductRendered(int id);
        bool Any(string barCode, int id = 0, string? condition = null);

        int GetProductRenderedId();

    }
}
