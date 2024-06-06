using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
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

        public bool AddCustomer(TblCustomer customer)
            => customerRepository.AddCustomer(customer);

        public TblCustomer GetCustomer(string customerID)
            => customerRepository.GetCustomer(customerID);

        public List<TblCustomer> GetCustomers()
            => customerRepository.GetCustomers();

        public bool UpdateCustomer(string id, TblAccount customer)
            => customerRepository.UpdateCustomer(id, customer);
    }
}
