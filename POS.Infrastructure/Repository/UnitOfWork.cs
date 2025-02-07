using POS.Application.Common.Interfaces;
using POS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IProductBrandRepository ProductBrand { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProductTypeRepository ProductType { get; private set; }

        public IProductBrandTypeRepository ProductBrandType { get; private set; }

        public IProductPackagingRepository ProductPackaging { get; private set; }

        public IInventoryProductRepository InventoryProduct { get; private set; }

        public IProductRenderedRepository ProductRendered { get; private set; }

        public IProductDiscountTypeRepository ProductDiscountType { get; private set; }

        public IProductRenderedDiscountRepository ProductRenderedDiscount  { get; private set; }



        // public IProductRenderedRepository ProductRendered => throw new NotImplementedException();

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
            ProductBrand = new ProductBrandRepository(_db); 
            ProductType = new ProductTypeRepository(_db);
            ProductBrandType = new ProductBrandTypeRepository(_db);
            ProductPackaging = new ProductPackagingRepository(_db);
            InventoryProduct = new InventoryProductRepository(_db);
            ProductRendered = new ProductRenderedRepository(_db);
            ProductDiscountType = new ProductDiscountTypeRepository(_db);
            ProductRenderedDiscount = new ProductRenderedDiscountRepository(_db);
        }
       

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
