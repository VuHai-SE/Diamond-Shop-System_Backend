using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class WarrantyRequest
    {
        public int OrderDetailID { get; set; }
        public string CustomerName { get; set; }
        public DateTime orderDate { get; set; }
        public string productID { get; set; }
        public string productName { get; set; }

    }
}
