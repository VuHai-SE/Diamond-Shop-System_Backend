using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class RefundRequest
    {
        public int PaymentID { get; set; }
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Reason { get; set; }
    }
}
