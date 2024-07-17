using BusinessObjects;
using DAOs.DTOs.Response;
using Services.DTOs.Request;
using Services.DTOs.Response;
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
        public Task<AccountInfo> GetAccountInfo(string username);

        public Task<StaffInfo> GetStaffInfo(string username);

        public Task<List<AccountInfo>> GetAccountInfoList();

        public Task<List<AccountInfo>> GetCustomerInfoList();

        public Task<List<StaffInfo>> GetSaleInfoList();

        public Task<List<StaffInfo>> GetShipperInfoList();
        public Task<bool> ChangeAccountRole(UpdateRoleRequest request);
        public Task<bool> UpdateAccountStatus(string username, bool status);
        public Task AddToStaffTables(string staffId, AccountInfo accountInfo);
        Task RegisterStaffAsync(RegisterStaff register);
        public Task<AccountCount> GetAccountCount();
        public Task<CustomerRankingCount> GetCustomerRankingCount();
    }
}
