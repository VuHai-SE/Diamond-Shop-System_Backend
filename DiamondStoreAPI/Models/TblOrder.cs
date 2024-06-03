using System;
using System.Collections.Generic;

namespace DiamondStoreAPI.Models;

public partial class TblOrder
{
    public string OrderId { get; set; } = null!;

    public string? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? ShippingDate { get; set; }

    public DateTime? ReceiveDate { get; set; }

    public string? StaffId { get; set; }

    public string? ShipperId { get; set; }

    public virtual TblCustomer? Customer { get; set; }

    public virtual TblStaff? Staff { get; set; }

    public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; } = new List<TblOrderDetail>();
}
