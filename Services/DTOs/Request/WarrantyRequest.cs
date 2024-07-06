using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class WarrantyRequest
    {
        public int OrderDetailID { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
