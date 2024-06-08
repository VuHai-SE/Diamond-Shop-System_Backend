using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class ProductRequest
    {
        public String ProductID {  get; set; }
        public String ProductName { get; set; }
        public int CustomizedSize { get; set; }
        public int Quantity { get; set; }
    }
}
