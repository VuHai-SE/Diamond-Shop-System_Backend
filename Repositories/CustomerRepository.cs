using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories
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
        public bool AddCustomer(TblCustomer customer)
            => customerDAO.AddCustomer(customer);

        public TblCustomer GetCustomer(string customerID)
            => customerDAO.GetCustomer(customerID);

        public List<TblCustomer> GetCustomers()
            => customerDAO.GetCustomers();

        public bool UpdateCustomer(string id, TblAccount customer)
            => customerDAO.UpdateCustomer(id, customer);
    }
}
