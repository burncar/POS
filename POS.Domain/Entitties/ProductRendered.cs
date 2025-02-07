using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace POS.Domain.Entitties
{
      
    public class ProductRendered
    {
        [NotMapped]
        private decimal _discount = 0;
        [NotMapped]
        private decimal _perItemPrice = 0;
        [NotMapped]
        private decimal _totalItemPrice = 0;
        [NotMapped]
        public Product Product { get; set; }
        [NotMapped]
        public bool Vatable { get; set; } //this id is from product 
        [NotMapped]
        public ProductRenderedDiscount ProductRenderedDiscount { get; set; }

        public int Id { get; set; }
        public int ProductRenderedId { get; set; }
        public int ProductId { get; set; } //etong id na ito is from inventory product id
        
       
        public string Barcode { get; set; }
        public int SmallQuantity { get; set; }
        public int BigQuantity { get; set; }
        public decimal PerItemPrice
        {
            get { return Math.Round(_perItemPrice, 2); }
            set { _perItemPrice = Math.Round(value, 2); }
        }

        public decimal TotalItemPrice
        {
            get { return Math.Round(_totalItemPrice, 2); }
            set { _totalItemPrice = Math.Round(value, 2); }
        }
        public decimal Discount 
        {
            get { return Math.Round(_discount, 2); }
            set { _discount = Math.Round(value, 2); }
        }
        public string Description { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; } = false;

    }
}
