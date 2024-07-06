using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

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

        public async Task<TblOrder> GetOrderById(int orderId)
        {
            return await dbContext.TblOrders.FindAsync(orderId);
        }

        public async Task<bool> UpdateOrder(TblOrder order)
        {
            dbContext.TblOrders.Update(order);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public List<TblOrder> GetOrders()
        {
            return dbContext.TblOrders.ToList();
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
                oldOrder.ShipStatus = "Cancel by customer";
                dbContext.TblOrders.Update(oldOrder);
                dbContext.SaveChanges();
            }
        }

        public async Task<List<TblOrder>> GetDeliveredOrdersByMonthAndYearAsync(int month, int year)
        {
            return await dbContext.TblOrders
                .Where(o => o.OrderDate.HasValue &&
                            o.OrderDate.Value.Month == month &&
                            o.OrderDate.Value.Year == year &&
                            o.OrderStatus == "Delivered")
                .ToListAsync();
        }
    }
}
