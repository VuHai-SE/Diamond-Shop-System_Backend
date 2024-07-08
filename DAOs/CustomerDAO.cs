using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class CustomerDAO
    {
        private readonly DiamondStoreContext dbContext;
        public CustomerDAO (DiamondStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public List<TblCustomer> GetCustomers()
            => dbContext.TblCustomers.ToList();

        public TblCustomer GetCustomerByID(int customerID)
            => dbContext.TblCustomers.FirstOrDefault(c => c.CustomerId.Equals(customerID));

        public TblCustomer GetCustomerByAccount(string username)
        {
            var account = dbContext.TblAccounts.FirstOrDefault(a => a.Username.Equals(username));
            return dbContext.TblCustomers.FirstOrDefault(c => c.AccountId.Equals(account.AccountId));
        }

        public LoginResponse GetCustomerByAccountForLogin (string username)
        {
            var account = dbContext.TblAccounts.FirstOrDefault(a => a.Username.Equals(username));
            var cus = dbContext.TblCustomers.FirstOrDefault(c => c.AccountId.Equals(account.AccountId));
            LoginResponse response = new LoginResponse();
            if (cus != null)
            {
                response.UserName = username;
                response.CustomerId = cus.CustomerId;
                response.LastName = cus.LastName;
                response.FirstName = cus.FirstName;
                response.Gender = cus.Gender;
                response.Birthday = cus.Birthday;
                response.Email = cus.Email;
                response.PhoneNumber = cus.PhoneNumber;
                response.Address = cus.Address;
                response.Ranking = cus.Ranking;
                response.DiscountRate = cus.DiscountRate;
                response.Status = cus.Status;
                response.Role = account.Role;
            }
            return response;
        }

        public TblCustomer AddCustomer(TblCustomer customer)
        {
            dbContext.TblCustomers.Add(customer);
            dbContext.SaveChanges();
            return customer;
        }

        //Update customer profile
        public async Task<GenericResponse> UpdateCustomerProfileAsync(int customerId, UpdateCustomerProfileRequest request)
        {
            var customer = await dbContext.TblCustomers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (customer == null)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "Customer not found."
                };
            }

            customer.FirstName = request.FirstName ?? customer.FirstName;
            customer.LastName = request.LastName ?? customer.LastName;
            customer.Gender = request.Gender ?? customer.Gender;
            customer.Birthday = request.Birthday ?? customer.Birthday;
            customer.PhoneNumber = request.PhoneNumber ?? customer.PhoneNumber;
            customer.Address = request.Address ?? customer.Address;

            dbContext.TblCustomers.Update(customer);
            await dbContext.SaveChangesAsync();

            return new GenericResponse
            {
                Success = true,
                Message = "Customer profile updated successfully."
            };
        }


        public bool IsEmailExisted(string email)
            => dbContext.TblCustomers.Any(c => c.Email.Equals(email));

        public bool isPhoneExisted(string phone)
            => dbContext.TblCustomers.Any(c => c.PhoneNumber.Equals(phone));

        public void UpdateCustomer(TblCustomer customer)
        {
            dbContext.TblCustomers.Update(customer);
            dbContext.SaveChanges();
        }
    }
}
