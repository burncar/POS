using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS.Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        DbSet<ProductBrand> ProductBrands { get; set; }
        DbSet<ProductType>  ProductTypes { get; set; }
        DbSet<ProductBrandType> ProductBrandTypes { get; set; }
        DbSet<ProductPackaging> ProductPackagings { get; set; }
        public DbSet<InventoryProduct> InventoryProducts { get; set; }
        public DbSet<ProductRendered>  ProductRendereds { get; set; }

        DbSet<ProductRenderedDiscount> ProductRenderedDiscounts { get; set; }
        DbSet<ProductDiscountType> productDiscountTypes { get; set; }
        DbSet<ProductRenderedDiscount> ProductRenderedDiscount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("VARCHAR(450)");
                entity.Property(e => e.Name).HasColumnType("VARCHAR(256)");
                entity.Property(e => e.NormalizedName).HasColumnType("VARCHAR(256)");
                entity.Property(e => e.ConcurrencyStamp).HasColumnType("TEXT");
            });
        }
    }

}
