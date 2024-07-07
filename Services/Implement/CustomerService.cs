using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
        }

        public TblCustomer GetCustomerByAccount(string username)
            => customerRepository.GetCustomerByAccount(username);

        public LoginResponse GetCustomerByAccountForLogin(string username)
            => customerRepository.GetCustomerByAccountForLogin(username);

        public TblCustomer GetCustomerByID(int customerID)
            => customerRepository.GetCustomerByID(customerID);

        public List<TblCustomer> GetCustomers()
            => customerRepository.GetCustomers();

        public Task<GenericResponse> UpdateCustomerProfileAsync(int customerId, UpdateCustomerProfileRequest request)
        => customerRepository.UpdateCustomerProfileAsync(customerId, request);

        public bool IsEmailExisted(string email)
            => customerRepository.IsEmailExisted(email);

        public bool isPhoneExisted(string phone)
            => customerRepository.isPhoneExisted(phone);
    }
}
