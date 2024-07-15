using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Response;

namespace DAOs
{
    public class OrderDAO
    {
        private readonly DiamondStoreContext dbContext;

        public OrderDAO(DiamondStoreContext _dbContext)
        {
            dbContext = _dbContext;
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

        public async Task<List<TblOrder>> GetOrders()
        {
            return await dbContext.TblOrders.AsNoTracking().ToListAsync();
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
                oldOrder.OrderNote = "Cancel by customer";
                dbContext.TblOrders.Update(oldOrder);
                dbContext.SaveChanges();
            }
        }

        public async Task<int> GetNumbersOrdersByMonthAndYearAsync(int? month = null, int? year = null)
        {
            
            if (month.HasValue && year.HasValue)
            {
                return await dbContext.TblOrders
                .CountAsync(o => o.OrderDate.HasValue &&
                            o.OrderDate.Value.Month == month &&
                            o.OrderDate.Value.Year == year);
            }
            return await dbContext.TblOrders.CountAsync();
        }

        public async Task<OrderStatusCount> GetOrderStatusCountAsync()
        {
            var all = await dbContext.TblOrders.CountAsync();
            var processing = await dbContext.TblOrders.CountAsync(o => o.OrderStatus == "Processing");
            var accepted = await dbContext.TblOrders.CountAsync(o => o.OrderStatus == "Accepted");
            var pendingDelivery = await dbContext.TblOrders.CountAsync(o => o.OrderStatus == "Pending Delivery");
            var deliverying = await dbContext.TblOrders.CountAsync(o => o.OrderStatus == "Deliverying");
            var delivered = await dbContext.TblOrders.CountAsync(o => o.OrderStatus == "Deliveried");
            var cancelled = await dbContext.TblOrders.CountAsync(o => o.OrderStatus == "Cancelled");

            return new OrderStatusCount()
            {
                All = all,
                Processing = processing,
                Accepted = accepted,
                PendingDelivery = pendingDelivery,
                Deliverying = deliverying,
                Delivered = delivered,
                Cancelled = cancelled
            };
        }

        public async Task<decimal> GetTotalRevenueAsync(int? month = null, int? year = null)
        {
            var query = dbContext.TblOrderDetails
                .Join(dbContext.TblOrders,
                      orderDetail => orderDetail.OrderId,
                      order => order.OrderId,
                      (orderDetail, order) => new { orderDetail, order })
                .Where(o => o.order.OrderStatus == "Deliveried");

            if (month.HasValue && year.HasValue)
            {
                query = query.Where(o => o.order.OrderDate.HasValue &&
                                         o.order.OrderDate.Value.Month == month.Value &&
                                         o.order.OrderDate.Value.Year == year.Value);
            }

            // Log the query result count
            var queryResult = await query.ToListAsync();
            Console.WriteLine($"Query with month: {month}, year: {year}");
            Console.WriteLine($"Number of records found: {queryResult.Count}");

            // Log the FinalPrice values for debugging
            foreach (var item in queryResult)
            {
                Console.WriteLine($"OrderDetailID: {item.orderDetail.OrderDetailId}, FinalPrice: {item.orderDetail.FinalPrice}");
            }

            var totalRevenue = queryResult.Sum(o => (decimal)o.orderDetail.FinalPrice);
            Console.WriteLine($"Total Revenue: {totalRevenue}");

            return totalRevenue;
        }
        
    }
}
