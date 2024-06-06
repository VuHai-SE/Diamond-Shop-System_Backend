using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAOs;

namespace Services.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
