using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class CustomerDAO
    {
        private readonly DiamondStoreContext dbContext = null;
        public CustomerDAO ()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext ();
            }
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
            
        public TblCustomer AddCustomer(TblCustomer customer)
        {
            dbContext.TblCustomers.Add(customer);
            dbContext.SaveChanges();
            return customer;
        }

        public bool IsEmailExisted(string email)
            => dbContext.TblCustomers.Any(c => c.Email.Equals(email));

        public bool isPhoneExisted(string phone)
            => dbContext.TblCustomers.Any(c => c.PhoneNumber.Equals(phone));
    }
}
