using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using POS.Domain.Entitties;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Web.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? BarCode { get; set; }
        public string? ProductDescription { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime DatePurchased { get; set; }
        public decimal Price { get; set; }
        public decimal WholeSellingPrice { get; set; }
        public decimal PerPieceSellingPrice { get; set; }

    
        public int? ProductBrandTypeId { get; set; }

     
        public bool IsActive { get; set; } = true;
    }
}
