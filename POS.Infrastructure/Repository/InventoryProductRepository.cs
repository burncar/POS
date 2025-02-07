using Microsoft.EntityFrameworkCore;
using POS.Application.Common.Interfaces;
using POS.Domain.Entitties;
using POS.Domain.Entitties.DTO;
using POS.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Repository
{
    public class InventoryProductRepository :Repository<InventoryProduct>, IInventoryProductRepository
    {
        private readonly ApplicationDbContext _db;
        public InventoryProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<InventoryProductDto> GetAllExistingQuantity()
        {
            var dataInventory = _db.InventoryProducts
                 .Include(a => a.Product)
                 .Include(a => a.ProductPackaging)
                 .GroupBy(a => new { a.ProductId, a.ProductPackagingId })
                 .Where(a => a.Count() >= 1)
                 .Select(a => new
                 {
                     InventoryProductId = a.First().Id,
                     ProductId = a.Key.ProductId,
                     ProductPackagingId = a.Key.ProductPackagingId,
                     Quantity = a.Sum(a => a.Quantity),
                     Product =a.First().Product, // Include Product details
                     ProductPackaging = a.First().ProductPackaging // Include Packaging details
                 }).ToList();

            var renderedData = _db.ProductRendereds
                .GroupBy(a => a.ProductId)
                .Where(a => a.Count() >= 1)
                .Select(a => new
                {
                    InventoryProductId = a.First().ProductId,
                    TotalQuantity = a.Sum(a => a.BigQuantity)
                }).ToList();

            var finalData = from inventory in dataInventory
                            join
                                 rendered in renderedData on
                                 inventory.InventoryProductId equals rendered.InventoryProductId into joined
                            from j in joined.DefaultIfEmpty() //this is left join
                            select new
                            {
                                inventory.Product,
                                inventory.ProductPackaging,
                                inventory.InventoryProductId,
                                inventory.ProductId,
                                inventory.ProductPackagingId,
                                InventoryQuantity = inventory.Quantity,
                                Quantity = inventory.Quantity - (j?.TotalQuantity ?? 0) // Deduct quantities
                            };

            var dataToSend = new List<InventoryProductDto>();
            foreach ( var item in finalData)
            {
                var data = new InventoryProductDto();
                data.ProductId = item.ProductId;
                data.Product = item.Product;
                data.InventoryProductId = item.InventoryProductId;
                data.ProductPackagingId = item.ProductPackagingId;
                data.ProductPackaging= item.ProductPackaging;
                data.Quantity = item.Quantity;
                dataToSend.Add(data);
            }

            return dataToSend;
        }

        public void Update(InventoryProduct inventoryProduct)
        {
            _db.Update<InventoryProduct>(inventoryProduct);
        }
    }
}
