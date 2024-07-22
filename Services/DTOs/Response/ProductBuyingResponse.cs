using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class ProductBuyingResponse
    {
        public int OrderDetailID { get; set; }
        public string ProductID {  get; set; }
        public string ProductName { get; set; }
        public string Material {  get; set; }
        public string Image { get; set; }
        public int CustomizedSize { get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
    }
}
