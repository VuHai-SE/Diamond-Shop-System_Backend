using BusinessObjects;
using Services.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        Task<TblAccount> AuthenticateAsync(string username, string password);

        Task RegisterAsync(RegisterRequest register);

        Task<string> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<TblAccount> GetAccountByUsernameAsync(string username);

        public TblAccount GetAccountSaleStaff(string saleStaffID);

        public TblAccount GetAccountShipper(string shipperID);

        public bool IsUsernameExisted(string username);
        public TblAccount GetAccountByEmail(string email);
    }
}
