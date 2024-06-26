using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
        public decimal GemCost { get; set; }
        public decimal ProductionCost { get; set; }
        public decimal PriceRate { get; set; }
        public int ProductSize { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public int Gender { get; set; }  // Nhận giá trị -1, 0 hoặc 1
        public string GemId { get; set; }
        public string MaterialId { get; set; }
        public double Weight { get; set; }
    }
}
