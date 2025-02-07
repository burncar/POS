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
    public class ProductRenderedDiscountRepository: Repository<ProductRenderedDiscount>, IProductRenderedDiscountRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRenderedDiscountRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(ProductRenderedDiscount productRenderedDiscount)
        {
            _db.Update<ProductRenderedDiscount>(productRenderedDiscount);
        }
    }
}
