using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class NewOrderRequest
    {
        public string Username { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public List<ProductRequest> Products { get; set; } = new List<ProductRequest>();

        public double Deposits { get; set; } = 0;

        public string TransactionId { get; set; }
        public string PayerEmail { get; set; }
        public string PaymentStatus { get; set; }
    }
}
