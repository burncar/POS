using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
    public class InventoryProduct
    {
        private decimal _WholePrice;
        private decimal _PerPiecePrice;
     
        public int Id { get; set; }
        public decimal WholePrice 
        {
            get { return Math.Round(_WholePrice, 2); }
            set { _WholePrice = Math.Round(value, 2); }
        }
        [NotMapped]
        public decimal PerPiecePrice
        {
            get { return Math.Round(_PerPiecePrice, 2); }
            set { _PerPiecePrice = Math.Round(value, 2); }
        }
      

        private DateTime _expirationDate;
        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set => _expirationDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        private DateTime _datePurchased;
        public DateTime DatePurchased
        {
            get => _datePurchased;
            set => _datePurchased = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        private double _productQuantity;
        public double Quantity
        {
            get { return Math.Round(_productQuantity, 2); }
            set { _productQuantity = Math.Round(value, 2); }
        }
        // public int Reorder_Level { get; set; }
       
        //public int ItemPerUnit { get; set; }

        [ForeignKey("ProductPackagingId")]
        public int ProductPackagingId { get; set; }
        [ValidateNever]
        public ProductPackaging ProductPackaging { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
    