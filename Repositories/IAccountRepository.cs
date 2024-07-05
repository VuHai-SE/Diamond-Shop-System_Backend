using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAccountRepository
    {
        Task<TblAccount> GetAccountByUsernameAsync(string username);

        Task AddAccountAsync(TblAccount account);

        Task UpdatePasswordAsync(string username, string newPassword);

        public TblAccount AddAccount(TblAccount account);

        Task AddAccountByManagerAsync(TblAccount account);
        public TblAccount GetAccountSaleStaff(string saleStaffID);
        public TblAccount GetAccountShipper(string shipperID);
        public bool IsUsernameExisted(string username);
        Task<TblAccount> GetAccountByEmailAsync(string email);
    }
}
