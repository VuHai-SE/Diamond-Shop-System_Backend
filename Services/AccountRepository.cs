using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
using Repositories;

namespace Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO accountDAO = null;

        public AccountRepository()
        {
            if (accountDAO == null)
            {
                accountDAO = new AccountDAO();
            }
        }

        public bool AddAccount(TblAccount account)
            => accountDAO.AddAccount(account);

        public TblAccount GetAccount(int id)
            => accountDAO.GetAccount(id);

        public List<TblAccount> GetAccounts()
            => accountDAO.GetAccounts();

        public bool UpdateAccount(int id, TblAccount account)
            => accountDAO.UpdateAccount(id, account);
    }
}
