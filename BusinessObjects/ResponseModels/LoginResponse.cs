using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class LoginResponse
    {
        public int CustomerId { get; set; }

        public string UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Ranking { get; set; }

        public double? DiscountRate { get; set; }

        public bool? Status { get; set; }
        public string Role { get; set; }
    }
}
