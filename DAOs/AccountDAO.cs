using BCrypt.Net;
using BusinessObjects;
using DAOs.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class AccountDAO
    {
        private readonly DiamondStoreContext _context;

        public AccountDAO(DiamondStoreContext context)
        {
            _context = context;
        }

        public List<TblAccount> GetAllAccount()
        {
            return _context.TblAccounts.ToList();
        }
        public TblAccount GetAccountByEmail(string email)
        {
            var customer = _context.TblCustomers.FirstOrDefault(c => c.Email.Equals(email));
            return _context.TblAccounts.FirstOrDefault(a => a.AccountId.Equals(customer.AccountId));
        }

        public async Task<TblAccount> GetAccountByUsernameAsync(string username)
        {
            return await _context.TblAccounts.AsNoTracking().FirstOrDefaultAsync(a => a.Username.Equals(username));
        }

        public async Task AddAccountAsync(TblAccount account)
        {
            var existingAccount = await _context.TblAccounts.AsNoTracking().FirstOrDefaultAsync(a => a.AccountId == account.AccountId);
            if (existingAccount == null)
            {
                await _context.TblAccounts.AddAsync(account);
            }
            else
            {
                _context.Entry(account).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public TblAccount AddAccount(TblAccount account)
        {
            _context.TblAccounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public async Task UpdatePasswordAsync(string username, string newPassword)
        {
            var account = await _context.TblAccounts.FirstOrDefaultAsync(a => a.Username.Equals(username));
            if (account != null)
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _context.TblAccounts.Update(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAccountByManagerAsync(TblAccount account)
        {
            _context.TblAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public TblAccount GetAccountSaleStaff(string saleStaffID)
        {
            var saleStaff = _context.TblSaleStaffs.FirstOrDefault(ss => ss.StaffId.Equals(saleStaffID));
            return _context.TblAccounts.FirstOrDefault(a => a.AccountId.Equals(saleStaff.AccountId));
        }

        public TblAccount GetAccountShipper(string shipperID)
        {
            var shipper = _context.TblShippers.FirstOrDefault(sh => sh.ShipperId.Equals(shipperID));
            return _context.TblAccounts.FirstOrDefault(a => a.AccountId.Equals(shipper.AccountId));
        }

        public bool IsUsernameExisted(string username)
            => _context.TblAccounts.Any(a => a.Username.Equals(username));
        public List<TblAccount> GetAllStaff()
        {
            return _context.TblAccounts.Where(a => a.Role == "Staff").ToList();
        }

        public bool UpdateAccount(TblAccount account)
        {
            var existingAccount = _context.TblAccounts.AsNoTracking().FirstOrDefault(a => a.AccountId == account.AccountId);
            if (existingAccount != null)
            {
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<AccountCount> GetAccountCount()
        {
            var all = await _context.TblAccounts.CountAsync();
            var manager = await _context.TblAccounts.CountAsync(a => a.Role == "Manager");
            var customer = await _context.TblAccounts.CountAsync(a => a.Role == "Customer");
            var saleStaff = await _context.TblAccounts.CountAsync(a => a.Role == "SaleStaff");
            var shipper = await _context.TblAccounts.CountAsync(a => a.Role == "Shipper");
            return new AccountCount()
            {
                All = all,
                Manager = manager,
                Customer = customer,
                SaleStaff = saleStaff,
                Shipper = shipper
            };
        }

        public async Task<CustomerRankingCount> GetCustomerRankingCount()
        {
            var bronzeCount = await _context.TblCustomers.CountAsync(c => c.Ranking == "Bronze" && c.Account.Role == "Customer");
            var silverCount = await _context.TblCustomers.CountAsync(c => c.Ranking == "Silver" && c.Account.Role == "Customer");
            var goldCount = await _context.TblCustomers.CountAsync(c => c.Ranking == "Gold" && c.Account.Role == "Customer");
            var platinumCount = await _context.TblCustomers.CountAsync(c => c.Ranking == "Platinum" && c.Account.Role == "Customer");
            var diamondCount = await _context.TblCustomers.CountAsync(c => c.Ranking == "Diamond" && c.Account.Role == "Customer");

            return new CustomerRankingCount
            {
                Bronze = bronzeCount,
                Silver = silverCount,
                Gold = goldCount,
                Platinum = platinumCount,
                Diamond = diamondCount
            };
        }
    }
}
