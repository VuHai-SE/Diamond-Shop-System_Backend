using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class OrderDAO
    {
        private readonly DiamondStoreContext dbContext = null;

        public OrderDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public TblOrder AddOrder(TblOrder order)
        {
            dbContext.TblOrders.Add(order);
            dbContext.SaveChanges();
            return order;
        }

        public List<TblOrder> getOrderByCustomerID(string customerID)
        {
            if (!string.IsNullOrEmpty(customerID))
            {
                dbContext.TblOrders.Where(o => o.CustomerId.Equals(customerID)).ToList();
            }
            return new List<TblOrder>();
        }

    }
}
