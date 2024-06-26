using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class GemPriceResponse
    {
        public int Id { get; set; }

        public string? Origin { get; set; }

        public double? CaratWeight { get; set; }

        public string? Color { get; set; }

        public string? Cut { get; set; }

        public string? Clarity { get; set; }

        public double? Price { get; set; }

        public DateTime? EffDate { get; set; }
    }
}
