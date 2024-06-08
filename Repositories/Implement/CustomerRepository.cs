using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
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
        public TblCustomer GetCustomerByID(int customerID)
            => customerDAO.GetCustomerByID(customerID);

        public List<TblCustomer> GetCustomers()
            => customerDAO.GetCustomers();
    }
}
