using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblPayment
{
    public string? OrderId { get; set; }

    public string? CustomerId { get; set; }

    public string? PaymentMethod { get; set; }

    public double? Deposits { get; set; }

    public string? PayDetail { get; set; }

    public virtual TblCustomer? Customer { get; set; }

    public virtual TblOrder? Order { get; set; }
}
