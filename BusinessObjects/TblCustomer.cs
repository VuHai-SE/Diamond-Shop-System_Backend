using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblCustomer
{
    public string CustomerId { get; set; } = null!;

    public string? AccountId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Ranking { get; set; }

    public double? DiscountRate { get; set; }

    public bool? Status { get; set; }

    public virtual TblAccount? Account { get; set; }

    public virtual ICollection<TblOrder> TblOrders { get; set; } = new List<TblOrder>();
}
