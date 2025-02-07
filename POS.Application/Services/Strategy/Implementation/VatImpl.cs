using POS.Application.Services.Strategy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Strategy.Implementation
{
    public class VatImpl : IVat
    {
        public decimal GetVat(decimal amount)
        {
            return Math.Round(amount / 1.12m, 2);
        }
    }
}
