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

        public TblAccount AddAccount(TblAccount account);

        Task AddAccountByManagerAsync(TblAccount account);
        public TblAccount GetAccountSaleStaff(string saleStaffID);
        public TblAccount GetAccountShipper(string shipperID);
    }
}
