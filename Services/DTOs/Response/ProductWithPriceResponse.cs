using BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string? CategoryID { get; set; } //add mới cho Triều
        public decimal? GemCost { get; set; } //add mới cho Triều
        public decimal? ProductionCost { get; set; } //add mới cho Triều
        public decimal? PriceRate { get; set; } //add mới cho Triều
        public string? GemId { get; set; } //add mới cho Triều
        public string? MaterialId { get; set; } //add mới cho Triều
        public double? Weight { get; set; } //add mới cho Triều
        public string? Category { get; set; }
        public string? Material {  get; set; }
        public string? GemOrigin { get; set; }
        public double? CaratWeight { get; set; }
        public string? Clarity { get; set; }
        public string? Color { get; set; }
        public string? Cut { get; set; }
        public int? ProductSize { get; set; }
        public string? Image { get; set; }
        public bool? Status { get; set; }
        public double? UnitSizePrice { get; set; }
        public string? Gender { get; set; }
        public double? ProductPrice { get; set; }
        [NotMapped]
        public Task<double> CalculatePriceTask { get; set; } // Thuộc tính tạm thời để giữ Task
    }
}
