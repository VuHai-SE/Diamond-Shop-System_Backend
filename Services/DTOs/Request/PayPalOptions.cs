using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class PayPalOptions
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
}
