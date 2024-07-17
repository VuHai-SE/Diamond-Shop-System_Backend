using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblRefund
{
    public int RefundId { get; set; }

    public int? PaymentId { get; set; }

    public decimal? RefundAmount { get; set; }

    public string? RefundStatus { get; set; }

    public DateTime? RefundDate { get; set; }

    public string? Reason { get; set; }

    public virtual TblPayment? Payment { get; set; }
}
