using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public  class SaleStaffDAO
    {
        private readonly DiamondStoreContext _dbContext = null;

        public SaleStaffDAO()
        {
            if (_dbContext == null)
            {
                _dbContext = new DiamondStoreContext();
            }
        }

        public TblSaleStaff GetSaleStaffByUsername(string username)
        {
            var acc = _dbContext.TblAccounts.FirstOrDefault(a => a.Username.Equals(username));
            return _dbContext.TblSaleStaffs.FirstOrDefault(s => s.AccountId.Equals(acc.AccountId));
        }

        public bool isSaleStaffIdExist(string staffId)
            => _dbContext.TblAccounts.Any(s => s.Equals(staffId));
    }
}
