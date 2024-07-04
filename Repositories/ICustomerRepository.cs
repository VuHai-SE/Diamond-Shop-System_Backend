using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface ICustomerRepository
    {
        public List<TblCustomer> GetCustomers();
        public TblCustomer GetCustomerByID(int customerID);
        public TblCustomer GetCustomerByAccount(string username);
        public LoginResponse GetCustomerByAccountForLogin(string username);
        public TblCustomer AddCustomer(TblCustomer customer);
        Task<GenericResponse> UpdateCustomerProfileAsync(int customerId, UpdateCustomerProfileRequest request);
        public bool IsEmailExisted(string email);
        public bool isPhoneExisted(string phone);
        public void UpdateCustomer(TblCustomer customer);
    }
}
