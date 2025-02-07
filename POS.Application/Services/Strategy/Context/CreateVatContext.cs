using POS.Application.Services.Strategy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Strategy.Context
{
    public class CreateVatContext
    {
        private IVat _ivat;

        public CreateVatContext(IVat ivat)
        {
            _ivat = ivat;
        }
        public decimal ExecuteGetVat(decimal dec)
        {
            return _ivat.GetVat(dec);
        }

    }
}
