using POS.Domain.Entitties;
using POS.Domain.Entitties.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Common.Interfaces
{
    public interface IInventoryProductRepository :IRepository<InventoryProduct>
    {
        void Update(InventoryProduct inventoryProduct);

        IEnumerable<InventoryProductDto> GetAllExistingQuantity();
    }
}
