using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs.DTOs.Response
{
    public class CustomerRankingCount
    {
        public int Bronze { get; set; }
        public int Silver { get; set; }
        public int Gold {  get; set; }
        public int Platinum { get; set; }
        public int Diamond { get; set; }
    }
}
