using BusinessObjects;
using Microsoft.Extensions.Options;
using Repositories;
using Repositories.Implement;
using Services.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

        //public async Task<TblAccount> AuthenticateAsync(string username, string password)
        //{
        //    var account = await _accountRepository.GetAccountByUsernameAsync(username);

        //    if (account == null)
        //    {
        //        return null;
        //    }
        //    return account;
        //}

        public async Task<TblAccount> AuthenticateAsync(string username, string password)
        {
            var account = await _accountRepository.GetAccountByUsernameAsync(username);
            if (account == null || !BCrypt.Net.BCrypt.Verify(password, account.Password))
            {
                return null;
            }
            return account;
        }

        public async Task RegisterAsync(RegisterRequest register)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);

            var newAccount = new TblAccount
            {
                Username = register.Username,
                Password = passwordHash,
                Role = "Customer" // Mặc định role là Customer
            };

            await _accountRepository.AddAccountAsync(newAccount);

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
                DiscountRate = 0.02,
                Ranking = "Bronze",
                Status = true
            };
            _customerRepository.AddCustomer(customer);
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var account = await _accountRepository.GetAccountByUsernameAsync(request.Username);
            if (account == null)
            {
                return "Account not found.";
            }

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, account.Password))
            {
                return "Old password does not match.";
            }

            if (BCrypt.Net.BCrypt.Verify(request.NewPassword, account.Password))
            {
                return "New password cannot be the same as the old password.";
            }

            await _accountRepository.UpdatePasswordAsync(request.Username, request.NewPassword);
            return "Password updated successfully.";
        }


        public async Task<TblAccount> GetAccountByUsernameAsync(string username)
            => await _accountRepository.GetAccountByUsernameAsync(username);

        public TblAccount GetAccountSaleStaff(string saleStaffID)
            => _accountRepository.GetAccountSaleStaff(saleStaffID);

        public TblAccount GetAccountShipper(string shipperID)
            => _accountRepository.GetAccountShipper(shipperID);

        public bool IsUsernameExisted(string username)
            => _accountRepository.IsUsernameExisted(username);

        

    }
}
