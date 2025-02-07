using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entitties
{
    public class ProductPackaging
    {
        public int Id { get; set; }
        public string PackagingType { get; set; }

        public int ItemPerPiece { get; set; }

    }
}
