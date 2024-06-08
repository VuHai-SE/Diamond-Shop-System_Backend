using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class ProductResponse
    {
        public string ProductID {  get; set; }
        public string ProductName { get; set; }
        public int ProductSize { get; set; }
        public int Quantity {  get; set; }
    }
}
