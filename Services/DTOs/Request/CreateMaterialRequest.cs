using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class CreateMaterialRequest
    {
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public double UnitPrice { get; set; }
        public DateTime EffDate { get; set; }
    }
}
