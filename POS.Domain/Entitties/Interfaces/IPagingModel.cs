using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties.Interfaces
{
    public interface IPagingModel
    {
        int PageNumber { get; }
        int PageSize { get; }
        int TotalRecords { get; }
        string? SearchString { get; }
    }
}
