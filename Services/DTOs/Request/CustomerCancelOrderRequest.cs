using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class CustomerCancelOrderRequest
    {
        public int OrderID { get; set; }
        public string Note { get; set; }
    }
}
