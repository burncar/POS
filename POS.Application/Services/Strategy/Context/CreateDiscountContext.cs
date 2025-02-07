using POS.Application.Services.Strategy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Strategy.Context
{
    public class CreateDiscountContext
    {
        private IDiscountStrategy _discountStrategy;
        
        public CreateDiscountContext(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }
        public decimal ExecuteDiscount(decimal dec, decimal rate)
        {
            return _discountStrategy.Discount(dec, rate);
        }

      


    }
}
