using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class ProductWithPriceResponse
    {
        public TblProduct product { get; set; }
        public double price { get; set; }
    }
}
