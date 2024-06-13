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
        public TblCustomer GetCustomerByID(int customerID);
        public TblCustomer GetCustomerByAccount(string username);
    }
}
