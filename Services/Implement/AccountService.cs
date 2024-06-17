using BusinessObjects;
using Repositories;
using Repositories.Implement;
using Services.DTOs.Request;
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
        private readonly ICustomerRepository _customerRepository;

        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
           _accountRepository = accountRepository;
            _customerRepository = customerRepository;
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

        public async Task RegisterAsync(RegisterRequest register)
        {
           
            var account = new TblAccount
            {
                Username = register.Username,
                Password = register.Password,
                Role = "Customer" // Mặc định role là Customer
            };
            var newAccount = _accountRepository.AddAccount(account);
            var customer = new TblCustomer()
            {
                AccountId = newAccount.AccountId,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Gender = (register.Gender.Equals("Male")) ? true : false,
                Birthday = register.Birthday,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                Address = register.Address,
                Ranking = "Bronze",
                DiscountRate = 0.02,
                Status = true
            };
            _customerRepository.AddCustomer(customer);
        }
    }
}
