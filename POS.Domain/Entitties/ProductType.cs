using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
    public class ProductType
    {
        //soap, detergent, insecticide
        public int Id { get; set; }
        //public int ProductId { get; set; }
        public string Type { get; set; }
        //public int? ProductBrandId { get; set; }
        //[ValidateNever]
        //public ProductBrand? ProductBrand { get; set; }

    }
}
