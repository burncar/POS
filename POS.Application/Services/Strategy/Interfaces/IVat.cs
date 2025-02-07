using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Strategy.Interfaces
{
    public interface IVat
    {
        public decimal GetVat(decimal amount);
    }
}
