using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class ShipperDAO
    {
        private readonly DiamondStoreContext _dbContext = null;

        public ShipperDAO()
        {
            if(_dbContext == null)
            {
                _dbContext = new DiamondStoreContext();
            }
        }

        public TblShipper GetShipperByUsername(string username)
        {
            var acc = _dbContext.TblAccounts.FirstOrDefault(a => a.Username.Equals(username));
            return _dbContext.TblShippers.FirstOrDefault(sh => sh.AccountId.Equals(acc.AccountId));
        }

        public bool IsShipperIdExist(string shipperId)
            => _dbContext.TblShippers.Any(s => s.Equals(shipperId));
    }
}
