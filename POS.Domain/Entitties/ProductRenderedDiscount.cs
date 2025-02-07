using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
   
    public class ProductRenderedDiscount
    {
        public int Id { get; set; }
        public int ProductRenderedId { get; set; }
        public decimal DiscountTotal { get; set; }
        public int ProductDiscountTypeId { get; set; }
    }
}
