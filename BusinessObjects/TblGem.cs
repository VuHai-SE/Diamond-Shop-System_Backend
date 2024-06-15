using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblGem
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

    public virtual TblDiamondGradingReport? TblDiamondGradingReport { get; set; }

    public virtual TblProductGem? TblProductGem { get; set; }
}
