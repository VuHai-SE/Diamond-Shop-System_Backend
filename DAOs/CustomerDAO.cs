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
        private readonly DiamondStoreContext dbContext = null;
        public CustomerDAO ()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext ();
            }
        }

        public List<TblCustomer> GetCustomers()
            => dbContext.TblCustomers.ToList();

        public TblCustomer GetCustomerByID(int customerID)
            => dbContext.TblCustomers.FirstOrDefault(c => c.CustomerId.Equals(customerID));

    }
}
