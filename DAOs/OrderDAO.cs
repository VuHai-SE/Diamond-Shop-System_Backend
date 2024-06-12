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

        public List<TblOrder> getOrderByCustomerID(int customerID)
        {
            return dbContext.TblOrders.Where(o => o.CustomerId.Equals(customerID)).ToList();
        }

        public TblOrder getOrderByOrderID(int orderID)
        {
            var order = dbContext.TblOrders.FirstOrDefault(o => o.OrderId.Equals(orderID));
            return order;
        }
           

        public void CancelOrder(int orderID)
        {
            TblOrder oldOrder = getOrderByOrderID(orderID);
            if (oldOrder != null)
            {
                oldOrder.OrderStatus = "Cancelled";
                dbContext.TblOrders.Update(oldOrder);
                dbContext.SaveChanges();
            }
        }
    }
}
