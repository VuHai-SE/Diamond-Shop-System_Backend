using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class UpdateDiamondGradingReportRequest
    {
        public DateTime? GenerateDate { get; set; }
        public string Image { get; set; }
    }
}
