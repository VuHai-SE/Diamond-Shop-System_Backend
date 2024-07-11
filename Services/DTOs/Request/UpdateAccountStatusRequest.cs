using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class UpdateAccountStatusRequest
    {
        public string Username { get; set; }
        public bool Status { get; set; }
    }
}
