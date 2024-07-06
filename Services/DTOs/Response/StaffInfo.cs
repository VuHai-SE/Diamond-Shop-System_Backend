using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class StaffInfo: AccountInfo
    {
        public string StaffId { get; set; }
    }
}
