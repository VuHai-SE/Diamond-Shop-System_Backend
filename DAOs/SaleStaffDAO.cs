using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public  class SaleStaffDAO
    {
        private readonly DiamondStoreContext _dbContext;

        public SaleStaffDAO(DiamondStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TblSaleStaff> GetSaleStaffByUsernameAsync(string username)
        {
            var acc = await _dbContext.TblAccounts.FirstOrDefaultAsync(a => a.Username.Equals(username));
            if (acc == null) return null;
            return await _dbContext.TblSaleStaffs.FirstOrDefaultAsync(s => s.AccountId.Equals(acc.AccountId));
        }

        public async Task<bool> IsSaleStaffIdExistAsync(string staffId)
        {
            return await _dbContext.TblAccounts.AnyAsync(s => s.Equals(staffId));
        }
    }
}
