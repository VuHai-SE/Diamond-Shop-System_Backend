using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class MaterialResponse
    {
        public string materialID {  get; set; }
        public string? materialName { get; set; }
        public double? UnitPrice { get; set; }
        public DateTime? EffectedDate { get; set; }
    }
}
