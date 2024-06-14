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

            if (account == null)
            {
                return null;
            }
            return account;
        }

        public async Task<TblAccount> GetAccountByUsernameAsync(string username)
            => await _accountRepository.GetAccountByUsernameAsync(username);

        public TblAccount GetAccountSaleStaff(string saleStaffID)
            => _accountRepository.GetAccountSaleStaff(saleStaffID);

        public TblAccount GetAccountShipper(string shipperID)
            => _accountRepository.GetAccountShipper(shipperID);

        public async Task RegisterAsync(string username, string password)
        {
            

            var account = new TblAccount
            {
                Username = username,
                Password = password,
                Role = "Customer" // Mặc định role là Customer
            };

            await _accountRepository.AddAccountAsync(account);
        }
    }
}
