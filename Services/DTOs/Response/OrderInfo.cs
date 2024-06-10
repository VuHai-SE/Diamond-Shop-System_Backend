using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DTOs.Request;

namespace Services.DTOs.Response
{
    public class OrderInfo
    {
        public int OrderID { get; set; }
        public List<ProductBuyingResponse> products { get; set; } = new List<ProductBuyingResponse>();
        public double TotalPrice { get; set; } = 0;
        public double DiscountRate { get; set; }
        public double FinalPrice { get; set; } = 0;
    }
}
