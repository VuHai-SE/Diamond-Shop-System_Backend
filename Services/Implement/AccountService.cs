using Azure.Core;
using BusinessObjects;
using DAOs.DTOs.Response;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Repositories;
using Repositories.Implement;
using Services.DTOs.Request;
using Services.DTOs.Response;
using SixLabors.ImageSharp.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISaleStaffRepository _saleStaffRepository;
        private readonly IShipperRepository _shipperRepository;

        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository, ISaleStaffRepository saleStaffRepository, IShipperRepository shipperRepository)
        {
           _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _saleStaffRepository = saleStaffRepository;
            _shipperRepository = shipperRepository;
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
            TblAccount account = null;
            var accUsername = await _accountRepository.GetAccountByUsernameAsync(username);
            
            if (accUsername != null)
            {
                account = accUsername;
            } else
            {
                var accEmail = _accountRepository.GetAccountByEmail(username);
                if (accEmail != null)
                {
                    account = accEmail;
                }
            }
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

        public async Task RegisterStaffAsync(RegisterStaff register)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);

            var newAccount = new TblAccount
            {
                Username = register.Username,
                Password = passwordHash,
                Role = register.Role // Mặc định role là Customer
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
            var createdAccount = await _accountRepository.GetAccountByUsernameAsync(register.Username);
            if (register.Role == "SaleStaff")
            {
                var lastSaleStaffId = _saleStaffRepository.GetLastStaffId();
                var numericPart = lastSaleStaffId.Substring(1);
                var nextNumericPart = (int.Parse(numericPart) + 1).ToString("D3");
                var nextId = $"S{nextNumericPart}";
                var sale = new TblSaleStaff()
                {
                    StaffId = nextId,
                    AccountId = createdAccount.AccountId,
                    FirstName = register.FirstName,
                    LastName= register.LastName,
                };
                await _saleStaffRepository.AddSaleStaffAsync(sale);
            } else if (register.Role == "Shipper")
            {
                var lastShipperId = _shipperRepository.GetLastShipperId();
                var numericPart = lastShipperId.Substring(2);
                var nextNumericPart = (int.Parse(numericPart) + 1).ToString("D3");
                var nextId = $"SP{nextNumericPart}";
                var shipper = new TblShipper()
                {
                    ShipperId = nextId,
                    AccountId = createdAccount.AccountId,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                };
                await _shipperRepository.AddShipperAsync(shipper);
            }
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

        public TblAccount GetAccountByEmail(string email)
            => _accountRepository.GetAccountByEmail(email);

        public async Task<AccountInfo> GetAccountInfo(string username)
        {
            var account = await _accountRepository.GetAccountByUsernameAsync(username);
            var detailInfo = _customerRepository.GetCustomerByAccount(username);
            if (account == null && detailInfo == null) { return null; }
            return new AccountInfo()
            {
                AccountId = account.AccountId,
                UserName = username,
                FirstName = detailInfo.FirstName,
                LastName = detailInfo.LastName,
                Role = account.Role,
                Gender = (detailInfo.Gender == true) ? "Male" : "Female",
                Birthday = detailInfo.Birthday,
                Email = detailInfo.Email,
                PhoneNumber = detailInfo.PhoneNumber,
                Address = detailInfo.Address,
                Ranking = detailInfo.Ranking,
                DiscountRate = detailInfo.DiscountRate,
                Status = detailInfo.Status,
            };
        }

        public async Task<StaffInfo> GetStaffInfo(string username)
        {
            var accountInfo = await GetAccountInfo(username);
            if (accountInfo == null) return null;
            var staffInfo = new StaffInfo()
            {
                AccountId = accountInfo.AccountId,
                UserName = accountInfo.UserName,
                FirstName = accountInfo.FirstName,
                LastName = accountInfo.LastName,
                Role = accountInfo.Role,
                Gender = accountInfo.Gender,
                Birthday = accountInfo.Birthday,
                Email = accountInfo.Email,
                PhoneNumber = accountInfo.PhoneNumber,
                Address = accountInfo.Address,
                Ranking = accountInfo.Ranking,
                DiscountRate = accountInfo.DiscountRate,
                Status = accountInfo.Status
            };
            if (accountInfo.Role == "SaleStaff")
            {
                var saleStaff = _saleStaffRepository.GetSaleStaffByUsername(username);
                staffInfo.StaffId = saleStaff.StaffId;
            }
            else if (accountInfo.Role == "Shipper")
            {
                var shipper = _shipperRepository.GetShipperByUsername(username);
                staffInfo.StaffId = shipper.ShipperId;
            }
            return staffInfo;
        }

        public async Task<List<AccountInfo>> GetAccountInfoList()
        {
            var accountInfoList = new List<AccountInfo>();
            var accountList = _accountRepository.GetAllAccount();
            foreach (var a in accountList)
            {
                var acc = await GetAccountInfo(a.Username);
                accountInfoList.Add(acc);
            }
            return accountInfoList;
        }

        public async Task<List<AccountInfo>> GetCustomerInfoList()
        {
            var customerInfoList = new List<AccountInfo>();
            var accountList = _accountRepository.GetAllAccount();
            foreach (var a in accountList)
            {
                if (a.Role.Equals("Customer"))
                {
                    var cus = await GetAccountInfo(a.Username);
                    customerInfoList.Add(cus);
                }
            }
            return customerInfoList;
        }

        public async Task<List<StaffInfo>> GetSaleInfoList()
        {
            var saleStaffInfoList = new List<StaffInfo>();
            var accountList = _accountRepository.GetAllAccount();
            foreach (var a in accountList)
            {
                if (a.Role == "SaleStaff")
                {
                    var sale = await GetStaffInfo(a.Username);
                    saleStaffInfoList.Add(sale);
                }
            }
            return saleStaffInfoList;
        }

        public async Task<List<StaffInfo>> GetShipperInfoList()
        {
            var shipperInfoList = new List<StaffInfo>();
            var accountList = _accountRepository.GetAllAccount();
            foreach (var a in accountList)
            {
                if (a.Role == "Shipper")
                {
                    var ship = await GetStaffInfo(a.Username);
                    shipperInfoList.Add(ship);
                }
            }
            return shipperInfoList;
        }

        public async Task AddToStaffTables(string staffId, AccountInfo accountInfo)
        {
            if (accountInfo.Role == "SaleStaff")
            {
                var sale = new TblSaleStaff()
                {
                    StaffId = staffId,
                    AccountId = accountInfo.AccountId,
                    FirstName = accountInfo.FirstName,
                    LastName = accountInfo.LastName,
                };
                await _saleStaffRepository.AddSaleStaffAsync(sale);
            }
            else if (accountInfo.Role == "Shipper")
            {
                var shipper = new TblShipper()
                {
                    ShipperId = staffId,
                    AccountId = accountInfo.AccountId,
                    FirstName = accountInfo.FirstName,
                    LastName = accountInfo.LastName,
                };
                await _shipperRepository.AddShipperAsync(shipper);
            }
        }

        public async Task<bool> ChangeAccountRole(UpdateRoleRequest request)
        {
            var account = await _accountRepository.GetAccountByUsernameAsync(request.UsertName);
            if (account == null) return false;
            account.Role = request.Role.Trim();
            var isUpdate = _accountRepository.UpdateAccount(account);
            return isUpdate;
        }

        public async Task<bool> UpdateAccountStatus(string username, bool status)
        {
            var account = await GetAccountByUsernameAsync(username);
            if (account == null) return false;
            var accountDetail = _customerRepository.GetCustomerByAccount(username);
            accountDetail.Status = status;
            _customerRepository.UpdateCustomer(accountDetail);
            return true;
        }

        public async Task<AccountCount> GetAccountCount()
            => await _accountRepository.GetAccountCount();
        public async Task<CustomerRankingCount> GetCustomerRankingCount()
            => await _accountRepository.GetCustomerRankingCount();
    }
}
