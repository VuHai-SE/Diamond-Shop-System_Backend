using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class AccountDAO
    {
        private readonly DiamondStoreContext dbContext;

        public AccountDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public List<TblAccount> GetAccounts()
            => dbContext.TblAccounts.ToList();

        public TblAccount GetAccount(int id)
            => dbContext.TblAccounts.Include(a => a.TblCustomer)
                        .Include(a => a.TblSaleStaff)
                        .Include(a => a.TblShipper).FirstOrDefault(a => a.AccountId.Equals(id));

        public bool AddAccount(TblAccount account)
        {
            return false;
        }

        public bool UpdateAccount(int id, TblAccount account)
        {
            return false;
        }
    }
}
