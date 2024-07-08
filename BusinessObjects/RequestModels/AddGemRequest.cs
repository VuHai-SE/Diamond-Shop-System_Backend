using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class AddGemRequest
    {
        public string GemId { get; set; } = null!;

        public string? GemName { get; set; }

        public string? Polish { get; set; }

        public string? Symmetry { get; set; }

        public string? Fluorescence { get; set; }

        public bool? Origin { get; set; }

        public double? CaratWeight { get; set; }

        public string? Color { get; set; }

        public string? Cut { get; set; }

        public string? Clarity { get; set; }

        public string? Shape { get; set; }

        public DateTime? GenerateDate { get; set; }

        public string? Image { get; set; }
    }
}
