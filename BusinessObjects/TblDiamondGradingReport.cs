using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblDiamondGradingReport
{
    public int ReportId { get; set; }

    public string? GemId { get; set; }

    public DateTime? GenerateDate { get; set; }

    public string? Image { get; set; }

    public virtual TblGem? Gem { get; set; }
}
