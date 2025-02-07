using POS.Domain.Entitties;
using POS.Domain.Entitties.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Interfaces
{
    public interface IInventoryProductService
    {
        IEnumerable<InventoryProduct> GetAllInventoryProducts();
        InventoryProduct GetInventoryProductById(int id);
        void CreateInventoryProduct(InventoryProduct inventoryProduct);
        void UpdateInventoryProduct(InventoryProduct inventoryProduct);
        bool DeleteInventoryProduct(int id);

        IEnumerable<InventoryProductDto> GetAllExistingInventoryQuantity();
        //bool Any(int productBrandId = 0, int productTypeId = 0, int id = 0, string? condition = null);
    }
}
