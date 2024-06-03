using System;
using System.Collections.Generic;

namespace DiamondStoreAPI.Models;

public partial class TblAccount
{
    public string AccountId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<TblCustomer> TblCustomers { get; set; } = new List<TblCustomer>();

    public virtual TblStaff? TblStaff { get; set; }
}
