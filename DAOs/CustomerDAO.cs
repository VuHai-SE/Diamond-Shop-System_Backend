using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class CustomerDAO
    {
        private readonly DiamondStoreContext dbContext;

        public CustomerDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public List<TblCustomer> GetCustomers()
            => dbContext.TblCustomers.ToList();

        public TblCustomer GetCustomer(string customerID)
            => dbContext.TblCustomers.FirstOrDefault(c => c.CustomerId.Equals(customerID));

        public bool AddCustomer(TblCustomer customer)
        {
            return false;
        }

        public bool UpdateCustomer(string id, TblAccount customer)
        {
            return false;
        }
    }
}
