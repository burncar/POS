using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Common.Interfaces
{
    public interface IProductRenderedRepository : IRepository<ProductRendered>
    {
        void Update(ProductRendered productRendered);
        void AddMultiple(IEnumerable<ProductRendered> productRendered);

        IEnumerable<ProductRendered> ReprintReceipt();

        int GetLastInsertedId();
    }
}
