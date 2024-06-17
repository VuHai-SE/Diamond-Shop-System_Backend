using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface ICustomerRepository
    {
        public List<TblCustomer> GetCustomers();
        public TblCustomer GetCustomerByID(int customerID);
        public TblCustomer GetCustomerByAccount(string username);
        public TblCustomer AddCustomer(TblCustomer customer);
    }
}
