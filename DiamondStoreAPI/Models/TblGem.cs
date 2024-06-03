using System;
using System.Collections.Generic;

namespace DiamondStoreAPI.Models;

public partial class TblGem
{
    public string GemId { get; set; } = null!;

    public string? GemName { get; set; }

    public string? Polish { get; set; }

    public string? Symmetry { get; set; }

    public string? Fluorescence { get; set; }

    public string? Origin { get; set; }

    public double? CaratWeight { get; set; }

    public string? Color { get; set; }

    public string? Cut { get; set; }

    public string? Clarity { get; set; }

    public string? Shape { get; set; }

    public virtual TblDiamondGradingReport? TblDiamondGradingReport { get; set; }

    public virtual ICollection<TblProduct> Products { get; set; } = new List<TblProduct>();
}
