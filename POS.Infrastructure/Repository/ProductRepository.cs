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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Product GetProductByBarcode(string barcode = "")
        {
            var prod = _db.Products.Where(a => a.BarCode.Trim().ToLower() == barcode.Trim().ToLower())
                .Include(a => a.ProductBrandType).FirstOrDefault();
            return (Product)prod;
        }

        public void Update(Product brand)
        {
            _db.Update<Product>(brand);
        }
    }
}
