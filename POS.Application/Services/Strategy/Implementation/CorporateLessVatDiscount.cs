using POS.Application.Services.Strategy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Strategy.Implementation
{
    public class CorporateLessVatDiscount : IDiscountStrategy
    {
        public decimal Discount(decimal dec, decimal discountRate)
        {
            decimal totalDiscount = 0;
            if (dec != 0)
            {
                //decimal totalWithoutTax = dec / (decimal)1.12;
                totalDiscount = (dec * discountRate);
            }
            return totalDiscount;
        }
    }
}
