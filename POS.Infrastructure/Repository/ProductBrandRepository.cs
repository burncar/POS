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
    public class ProductBrandRepository : Repository<ProductBrand>, IProductBrandRepository
    { 
        private readonly ApplicationDbContext _db;
        public ProductBrandRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public void Update(ProductBrand brand)
        {
            _db.Update<ProductBrand>(brand);
        }
    }
}
