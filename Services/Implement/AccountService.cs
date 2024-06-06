using BusinessObjects;
using Repositories;
using Repositories.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }

        public async Task<TblAccount> AuthenticateAsync(string username, string password)
        {
            var account = await _accountRepository.GetAccountByUsernameAsync(username);
            if (account == null || !BCrypt.Net.BCrypt.Verify(password, account.Password))
            {
                return null;
            }
            return account;
        }

        public async Task RegisterAsync(string username, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var account = new TblAccount
            {
                Username = username,
                Password = passwordHash,
                Role = "Customer" // Mặc định role là Customer
            };

            await _accountRepository.AddAccountAsync(account);
        }
    }
}
