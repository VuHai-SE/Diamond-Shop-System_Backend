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
        private readonly DiamondStoreContext _dbContext;

        public ShipperDAO (DiamondStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TblShipper GetShipperByUsername(string username)
        {
            var acc = _dbContext.TblAccounts.FirstOrDefault(a => a.Username.Equals(username));
            return _dbContext.TblShippers.FirstOrDefault(sh => sh.AccountId.Equals(acc.AccountId));
        }
    }
}
