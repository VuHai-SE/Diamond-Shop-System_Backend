using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class GemPriceListFilterCriteria
    {
        public string? Origin { get; set; }

        public double? MinCaratWeight { get; set; }

        public double? MaxCaratWeight { get; set; }

        public string? Color { get; set; }

        public string? Cut { get; set; }

        public string? Clarity { get; set; }
    }
}
