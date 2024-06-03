using System;
using System.Collections.Generic;

namespace DiamondStoreAPI.Models;

public partial class TblDiamondGradingReport
{
    public string ReportId { get; set; } = null!;

    public string? GemId { get; set; }

    public DateTime? GenerateDate { get; set; }

    public string? Image { get; set; }

    public virtual TblGem? Gem { get; set; }
}
