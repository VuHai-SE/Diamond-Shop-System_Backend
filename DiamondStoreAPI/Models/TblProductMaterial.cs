using System;
using System.Collections.Generic;

namespace DiamondStoreAPI.Models;

public partial class TblProductMaterial
{
    public string ProductId { get; set; } = null!;

    public string MaterialId { get; set; } = null!;

    public double? Weight { get; set; }

    public virtual TblMaterialCategory Material { get; set; } = null!;

    public virtual TblProduct Product { get; set; } = null!;
}
