using POS.Application.Common.Interfaces;
using POS.Domain.Entitties;
using POS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Repository
{
    public class ProductPackagingRepository : Repository<ProductPackaging>,IProductPackagingRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductPackagingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductPackaging productPackaging)
        {
            _db.Update<ProductPackaging>(productPackaging);
        }
    }
}
