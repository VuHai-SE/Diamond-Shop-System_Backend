using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblWarranty
{
    public int WarrantyId { get; set; }

    public int? OrderDetailId { get; set; }

    public DateTime? WarrantyStartDate { get; set; }

    public DateTime? WarrantyEndDate { get; set; }

    public byte[]? WarrantyPdf { get; set; }

    public virtual TblOrderDetail? OrderDetail { get; set; }
}
