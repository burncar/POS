
using POS.Domain.Entitties.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
    public abstract class PagingModel 
    {
        //public int PageNumber => pageNumber;

        //public int PageSize => pageSize;

        //public int TotalRecords => totalRecords;
        //public string? SearchString => searchString;

        //protected int pageNumber { get; set; }
        //protected int pageSize { get; set; }
        //protected int totalRecords { get; set; }
        //protected string? searchString { get; set; }

        public int pageNumber { get; set; } // Read-write
        public int pageSize { get; set; }   // Read-write
        public int totalRecords { get; set; } // Read-write
        public string? searchString { get; set; } // Read-write


    }
}
