using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository = null;

        public CustomerService()
        {
            if (customerRepository == null)
            {
                customerRepository = new CustomerRepository();
            }
        }

        public TblCustomer GetCustomerByID(int customerID)
            => customerRepository.GetCustomerByID(customerID);

        public List<TblCustomer> GetCustomers()
            => customerRepository.GetCustomers();
    }
}
