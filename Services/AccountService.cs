using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository = null;

        public AccountService()
        {
            if (accountRepository == null)
            {
                accountRepository = new AccountRepository();
            }
        }

        public bool AddAccount(TblAccount account)
            => accountRepository.AddAccount(account);

        public TblAccount GetAccount(int id)
            => accountRepository.GetAccount(id);

        public List<TblAccount> GetAccounts()
            => accountRepository.GetAccounts();

        public bool UpdateAccount(int id, TblAccount account)
            => accountRepository.UpdateAccount(id, account);
    }
}
