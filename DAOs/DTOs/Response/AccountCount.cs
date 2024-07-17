using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs.DTOs.Response
{
    public class AccountCount
    {
        public int All {  get; set; }
        public int Manager { get; set; }
        public int Customer { get; set; }
        public int SaleStaff { get; set; }
        public int Shipper { get; set; }
    }
}
