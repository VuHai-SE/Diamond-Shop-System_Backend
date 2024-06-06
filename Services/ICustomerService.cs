using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface ICustomerService
    {
        public List<TblCustomer> GetCustomers();
        public TblCustomer GetCustomer(string customerID);

        public bool AddCustomer(TblCustomer customer);

        public bool UpdateCustomer(string id, TblAccount customer);
    }
}
