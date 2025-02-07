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
    public class ProductBrandTypeRepository :Repository<ProductBrandType>, IProductBrandTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductBrandTypeRepository(ApplicationDbContext db) :base(db) 
        {
            _db = db;
        }

        public void Update(ProductBrandType brandType)
        {
            _db.Update<ProductBrandType>(brandType);
        }
    }
}
