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
            return await _context.TblAccounts.AsNoTracking().FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task AddAccountAsync(TblAccount account)
        {
            _context.TblAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task AddAccountByManagerAsync(TblAccount account)
        {
            _context.TblAccounts.Add(account);
            await _context.SaveChangesAsync();
        }
    }
}
