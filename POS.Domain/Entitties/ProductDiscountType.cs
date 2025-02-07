using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
    public class ProductDiscountType
    {
        public int Id { get; set; }
        public string? DiscountIdentity { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountRate { get; set; }
    }
}
