using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblProductCategory
{
    public string CategoryId { get; set; } = null!;

    public string? CategoryName { get; set; }

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
