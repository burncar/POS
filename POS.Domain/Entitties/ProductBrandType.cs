using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
    public class ProductBrandType { 
        public int Id { get; set; }

        [ValidateNever]
        public ProductBrand ProductBrand { get; set; }
        [ValidateNever]
        public ProductType ProductType { get; set; }
        public int ProductBrandId { get; set; }
        public int ProductTypeId { get; set; }



    }
}
