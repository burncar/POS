using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using POS.Domain.Entitties;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entitties.DTO
{
    public class InventoryProductDto
    {
       
        public int InventoryProductId { get; set; }
  
        public double Quantity { get; set; }
        public int ProductId { get; set; }

        public int ProductPackagingId { get; set; }
        [ValidateNever]
            public ProductPackaging ProductPackaging { get; set; }

 
        [ValidateNever]
        public Product Product { get; set; }
      
    }
}
