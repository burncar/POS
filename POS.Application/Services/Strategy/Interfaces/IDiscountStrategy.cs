using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Strategy.Interfaces
{
    public interface IDiscountStrategy
    {
        decimal Discount(decimal dec, decimal discountRate);
        
    }
}
