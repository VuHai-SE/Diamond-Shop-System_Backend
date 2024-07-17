using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class OrderStatusCount
    {
        public int All { get; set; }
        public int Processing { get; set; }
        public int Accepted { get; set; }
        public int PendingDelivery { get; set; }
        public int Deliverying { get; set; }
        public int Delivered { get; set; }
        public int Cancelled { get; set; }
    }
}
