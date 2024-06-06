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
        public TblCustomer GetCustomer(string customerID);

        public bool AddCustomer(TblCustomer customer);

        public bool UpdateCustomer(string id, TblAccount customer);
    }
}
