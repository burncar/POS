using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
    public class ProductBrand
    {
        //bear brand, milo
        public int Id { get; set; }
        public string BrandName { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CountryOrigin { get; set; }
       
      
    }
}
