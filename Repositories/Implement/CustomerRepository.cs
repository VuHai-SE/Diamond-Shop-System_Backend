using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.ResponseModels;
using DAOs;

namespace Repositories.Implement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDAO customerDAO = null;

        public CustomerRepository()
        {
            if (customerDAO == null)
            {
                customerDAO = new CustomerDAO();
            }
        }

        public TblCustomer GetCustomerByAccount(string username)
            => customerDAO.GetCustomerByAccount(username);

        public LoginResponse GetCustomerByAccountForLogin(string username)
            => customerDAO.GetCustomerByAccountForLogin(username);

        public TblCustomer GetCustomerByID(int customerID)
            => customerDAO.GetCustomerByID(customerID);

        public List<TblCustomer> GetCustomers()
            => customerDAO.GetCustomers();

        public TblCustomer AddCustomer(TblCustomer customer)
            => customerDAO.AddCustomer(customer);

        public bool IsEmailExisted(string email)
            => customerDAO.IsEmailExisted(email);

        public bool isPhoneExisted(string phone)
            => customerDAO.isPhoneExisted(phone);
    }
}
