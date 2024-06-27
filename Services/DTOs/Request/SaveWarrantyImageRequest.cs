using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class SaveWarrantyImageRequest
    {
        public int WarrantyId { get; set; }
        public string Base64Image { get; set; }
    }
}
