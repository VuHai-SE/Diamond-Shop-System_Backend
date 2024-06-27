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

        public TblAccount AddAccount(TblAccount account)
        {
            return _accountDAO.AddAccount(account);
        }

        public async Task UpdatePasswordAsync(string username, string newPassword)
        {
            await _accountDAO.UpdatePasswordAsync(username, newPassword);
        }

        public async Task AddAccountByManagerAsync(TblAccount account)
        {
            _accountDAO.AddAccount(account);
        }

        public TblAccount GetAccountSaleStaff(string saleStaffID)
            => _accountDAO.GetAccountSaleStaff(saleStaffID);

        public TblAccount GetAccountShipper(string shipperID)
            => _accountDAO.GetAccountShipper(shipperID);

        public bool IsUsernameExisted(string username)
            => _accountDAO.IsUsernameExisted(username);
    }
}
