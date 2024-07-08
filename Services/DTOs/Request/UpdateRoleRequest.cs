using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class UpdateRoleRequest
    {
        public string UsertName { get; set; }
        public string Role {  get; set; }
    }
}
