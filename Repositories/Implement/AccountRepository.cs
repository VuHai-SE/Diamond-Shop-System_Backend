using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO;

        public AccountRepository()
        {
            _accountDAO = new AccountDAO();
        }

        public async Task<TblAccount> GetAccountByUsernameAsync(string username)
        {
            return await _accountDAO.GetAccountByUsernameAsync(username);
        }

        public async Task AddAccountAsync(TblAccount account)
        {
            await _accountDAO.AddAccountAsync(account);
        }

        public async Task AddAccountByManagerAsync(TblAccount account)
        {
            await _accountDAO.AddAccountAsync(account);
        }
    }
}
