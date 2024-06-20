using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class UpdateGemPriceRequest
    {
        public int ID { get; set; }
        public double? NewPrice { get; set; }
        public DateTime? EffectDate { get; set; }
    }
}
