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
    public class ProductDiscountTypeRepository : Repository<ProductDiscountType>, IProductDiscountTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductDiscountTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductDiscountType productDiscountType)
        {
           _db.Update<ProductDiscountType>(productDiscountType);
        }
    }
}
