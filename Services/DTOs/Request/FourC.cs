using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Services.DTOs.Request
{
    public class FourC
    {
        public double? MinCarat {  get; set; }
        public double? MaxCarat { get; set;}
        public string? Clarity { get; set; }
        public string? Color { get; set; }
        public string? Cut { get; set; }
    }
}
