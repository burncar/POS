using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductByBarcode(string barcode="");
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        bool DeleteProduct(int id);

        bool Any(string barCode, int id = 0, string? condition=null);
    }
}
