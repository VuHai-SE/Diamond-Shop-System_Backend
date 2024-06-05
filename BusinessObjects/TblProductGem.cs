using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblProductGem
{
    public int Id { get; set; }

    public string? ProductId { get; set; }

    public string? GemId { get; set; }

    public virtual TblGem? Gem { get; set; }

    public virtual TblProduct? Product { get; set; }
}
