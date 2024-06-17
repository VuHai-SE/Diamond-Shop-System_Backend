using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class AccountDAO
    {
        public readonly DiamondStoreContext _context;

        public AccountDAO()
        {
            _context = new DiamondStoreContext();
        }

        public async Task<TblAccount> GetAccountByUsernameAsync(string username)
        {
            return await _context.TblAccounts.AsNoTracking().FirstOrDefaultAsync(a => a.Username.Equals(username));
        }

        public TblAccount AddAccount(TblAccount account)
        {
            _context.TblAccounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public async Task AddAccountByManagerAsync(TblAccount account)
        {
            _context.TblAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public TblAccount GetAccountSaleStaff(string saleStaffID)
        {
            var saleStaff = _context.TblSaleStaffs.FirstOrDefault(ss => ss.StaffId.Equals(saleStaffID));
            return _context.TblAccounts.FirstOrDefault(a => a.AccountId.Equals(saleStaff.AccountId));
        }

        public TblAccount GetAccountShipper(string shipperID)
        {
            var shipper = _context.TblShippers.FirstOrDefault(sh => sh.ShipperId.Equals(shipperID));
            return _context.TblAccounts.FirstOrDefault(a => a.AccountId.Equals(shipper.AccountId));
        }

    }
}
