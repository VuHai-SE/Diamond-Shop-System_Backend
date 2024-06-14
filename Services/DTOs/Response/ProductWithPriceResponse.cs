using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Response
{
    public class ProductWithPriceResponse
    {
        public string ProductId { get; set; } = null!;

        public string? ProductName { get; set; }

        public string? ProductCode { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }
        public string? Material {  get; set; }

        public double? MaterialCost { get; set; }

        public double? GemCost { get; set; }

        public double? ProductionCost { get; set; }

        public double? PriceRate { get; set; }

        public int? ProductSize { get; set; }

        public string? Image { get; set; }

        public bool? Status { get; set; }

        public double? UnitSizePrice { get; set; }
        public double? ProductPrice { get; set; }
    }
}
