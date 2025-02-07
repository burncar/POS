using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties.Interfaces
{
    public abstract class GrandTotalSum
    {

    private decimal _GrandTotal = 0;
    public decimal GrandTotal
    {
        get { return Math.Round(_GrandTotal, 2); }
        set { _GrandTotal = Math.Round(value, 2); }
    }

}
}
