using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblPayment
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? CustomerId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? TransactionId { get; set; }

    public string? PayerEmail { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? PaymentDate { get; set; }

    public double? Deposits { get; set; }

    public string? PayDetail { get; set; }

    public virtual TblCustomer? Customer { get; set; }

    public virtual TblOrder? Order { get; set; }

    public virtual TblRefund? TblRefund { get; set; }
}
