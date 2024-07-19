using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblOrder
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? ShippingDate { get; set; }

    public DateTime? ReceiveDate { get; set; }

    public string? StaffId { get; set; }

    public string? ShipperId { get; set; }

    public string? OrderNote { get; set; }

    public virtual TblCustomer? Customer { get; set; }

    public virtual TblShipper? Shipper { get; set; }

    public virtual TblSaleStaff? Staff { get; set; }

    public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; } = new List<TblOrderDetail>();

    public virtual TblPayment? TblPayment { get; set; }
}
