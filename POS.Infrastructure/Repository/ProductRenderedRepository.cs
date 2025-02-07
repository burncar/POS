using Microsoft.EntityFrameworkCore;
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
    public class ProductRenderedRepository : Repository<ProductRendered> , IProductRenderedRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRenderedRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void AddMultiple(IEnumerable<ProductRendered> productRendered)
        {
            _db.ProductRendereds.AddRange(productRendered);
        }

        public int GetLastInsertedId()
        {
            if (_db.ProductRendereds.FirstOrDefault() == null)
            {
                return 1;
            }
           return _db.ProductRendereds.OrderByDescending(a => a.ProductRenderedId).FirstOrDefault().ProductRenderedId + 1;
        }

        public IEnumerable<ProductRendered> ReprintReceipt()
        {
            var rendered = _db.ProductRendereds.OrderByDescending(a => a.Id);
            if (rendered.Any())
            {
               return rendered.Include(a => a.ProductRenderedDiscount);
            }
           return  null;
        }

        public void Update(ProductRendered productRendered)
        {
            _db.Update<ProductRendered>(productRendered);
        }
    }
}
