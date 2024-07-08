using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Services
{
    public interface IShipperService
    {
        public TblShipper GetShipperByUsername(string username);
        public bool IsShipperIdExist(string shipperId);
    }
}
