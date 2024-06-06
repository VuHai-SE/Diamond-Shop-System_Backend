using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services.DTOs
{
    public class StaffDTO : AccountDTO
    {
        public string StaffId { get; set; }

        public int AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
