using POS.Application.Services.Strategy.Context;
using POS.Application.Services.Strategy.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services.Strategy
{
    public class GetDiscountAndVat
    {
        public string _discountString { get; private set; }
      
        private decimal _discountRate = 0;
        private decimal _total = 0;
        

        private static GetDiscountAndVat Instance = null;
        private static readonly object InstanceLock = new object();
        public static GetDiscountAndVat GetInstance(string discountString = "", decimal discountRate = 0, decimal total = 0)
        {
            lock(InstanceLock)
            {
                if(Instance == null)
                {
                    Instance = new GetDiscountAndVat();
                }
            }
            Instance. _discountString = discountString;
            Instance._discountRate = discountRate;
            Instance._total = total;
            return Instance;
        }
        //private GetDiscountAndVat(string discountString = "", decimal discountRate = 0, decimal total = 0)
        //{
        //    _discountString = discountString;
        //    _discountRate = discountRate;
        //    _total = total;
        //}
        public decimal ComputeDiscount()
        {
            decimal totalDiscount = 0;
            if (!string.IsNullOrEmpty(_discountString) && _discountRate > 0)
            {

                if (_discountString == "SC/PWD")
                {
                    CreateDiscountContext discountContext = new CreateDiscountContext(new ScPwdDiscount());
                    totalDiscount = discountContext.ExecuteDiscount(ComputeAmoutLessVat(), _discountRate);
                }
                else if (_discountString == "CorpLessVat")
                {
                    CreateDiscountContext discountContext = new CreateDiscountContext(new CorporateLessVatDiscount());
                    totalDiscount = discountContext.ExecuteDiscount(ComputeAmoutLessVat(), _discountRate);
                }
                else if (_discountString == "CorpWithVat")
                {
                    CreateDiscountContext discountContext = new CreateDiscountContext(new CorporateWithVatDiscount());
                    totalDiscount = discountContext.ExecuteDiscount(_total, _discountRate);
                }
            }
            return totalDiscount;
        }

        public decimal ComputeAmoutLessVat()
        {
            decimal Vat = 0;
            CreateVatContext createVatContext = new CreateVatContext(new VatImpl());
            Vat = createVatContext.ExecuteGetVat(_total);

            return Vat;
        }
        public decimal GetVat()
        {
            decimal Vat = 0;
            Vat = _total - ComputeAmoutLessVat();
            return Vat;
        }



    }
}
