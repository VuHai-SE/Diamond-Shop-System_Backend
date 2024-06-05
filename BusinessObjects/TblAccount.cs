using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TblAccount
{
    public int AccountId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual TblCustomer? TblCustomer { get; set; }

    public virtual TblSaleStaff? TblSaleStaff { get; set; }

    public virtual TblShipper? TblShipper { get; set; }
}
