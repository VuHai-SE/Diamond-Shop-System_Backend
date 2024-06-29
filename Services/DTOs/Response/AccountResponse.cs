using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    internal class AccountResponse
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

}
