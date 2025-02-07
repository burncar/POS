using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
    public class Product
    {
        private decimal _WholeSellingPrice;
        private decimal _PerPieceSellingPrice;
        public int Id { get; set; }
            public string? BarCode { get; set; }
            public string? ProductDescription { get; set; }
        //public DateTime ExpirationDate { get; set; }
        //public DateTime DatePurchased { get; set; }
        //public decimal Price { get; set; }
        //public decimal WholeSellingPrice { get; set; }
        //public decimal PerPieceSellingPrice { get; set; }
        public decimal WholeSellingPrice
        {
            get { return Math.Round(_WholeSellingPrice, 2); }
            set { _WholeSellingPrice = Math.Round(value, 2); }
        }
        public decimal PerPieceSellingPrice
        {
            get { return Math.Round(_PerPieceSellingPrice, 2); }
            set { _PerPieceSellingPrice = Math.Round(value, 2); }
        }

        [ForeignKey("ProductBrandTypeId")]
            public int? ProductBrandTypeId { get; set; }

            [ValidateNever]
            public ProductBrandType ProductBrandType { get; set; }
            public int ReorderLevel { get; set; }

            public bool IsActive { get; set; } = true;
            public bool Vatable { get; set; }

        //public ProductConversion? ProductConversion { get; set; }

        //public int? CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        //public ProductCategory? ProductCategory { get; set; }

    }
}
