using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblMaterialPriceList
{
    public string? MaterialId { get; set; }

    public double? UnitPrice { get; set; }

    public DateTime? EffDate { get; set; }

    public virtual TblMaterialCategory? Material { get; set; }
}
