using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class OrderStatusRequest
    {
        public int? OrderID { get; set; }
        public string? ButtonValue { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
    }
}
