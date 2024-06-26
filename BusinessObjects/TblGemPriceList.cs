using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblGemPriceList
{
    public int Id { get; set; }

    public bool? Origin { get; set; }

    public double? CaratWeight { get; set; }

    public string? Color { get; set; }

    public string? Cut { get; set; }

    public string? Clarity { get; set; }

    public double? Price { get; set; }

    public DateTime? EffDate { get; set; }

    public double? Size { get; set; }
}
