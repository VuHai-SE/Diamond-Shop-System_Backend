using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class WarrantyResponse
    {
        public int WarrantyID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Category { get; set; }
        public string Material {  get; set; }
        public double MaterialWeight { get; set; }
        public double GemCaratWeight { get; set; }
        public string GemOrigin { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
