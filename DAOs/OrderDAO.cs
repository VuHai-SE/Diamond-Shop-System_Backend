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

        public async Task<List<decimal>> GetRevenuePerMonthOfYear(int year)
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            var revenuePerMonth = await dbContext.TblOrderDetails
                .Join(dbContext.TblOrders,
                      orderDetail => orderDetail.OrderId,
                      order => order.OrderId,
                      (orderDetail, order) => new { orderDetail, order })
                .Where(o => o.order.OrderStatus == "Deliveried" && o.order.OrderDate.HasValue && o.order.OrderDate.Value.Year == year)
                .GroupBy(o => o.order.OrderDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(x => (decimal)x.orderDetail.FinalPrice)
                })
                .ToListAsync();

            int monthCount = (year == currentYear) ? currentMonth : 12;
            List<decimal> monthlyRevenues = new List<decimal>(new decimal[monthCount]);

            foreach (var revenue in revenuePerMonth)
            {
                if (revenue.Month <= monthCount)
                {
                    monthlyRevenues[revenue.Month - 1] = revenue.TotalRevenue;
                }
            }

            return monthlyRevenues;
        }

        public async Task<List<int>> GetNumberOrdersPerMonthOfYear(int year)
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            var ordersPerMonth = await dbContext.TblOrders
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == year && o.OrderStatus == "Deliveried")
                .GroupBy(o => o.OrderDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    OrderCount = g.Count()
                })
                .ToListAsync();

            int monthCount = (year == currentYear) ? currentMonth : 12;
            List<int> monthlyOrders = new List<int>(new int[monthCount]);

            foreach (var orders in ordersPerMonth)
            {
                if (orders.Month <= monthCount)
                {
                    monthlyOrders[orders.Month - 1] = orders.OrderCount;
                }
            }

            return monthlyOrders;
        }



        public async Task<decimal> GetTotalRevenueAsync()
        {
            try
            {
                // Sum directly in the query to improve efficiency
                var totalRevenue = await dbContext.TblOrderDetails
                    .Join(dbContext.TblOrders,
                          orderDetail => orderDetail.OrderId,
                          order => order.OrderId,
                          (orderDetail, order) => new { orderDetail, order })
                    .Where(o => o.order.OrderStatus == "Deliveried")
                    .SumAsync(o => (decimal?)o.orderDetail.FinalPrice) ?? 0;

                return totalRevenue;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while calculating total revenue: {ex.Message}");
                // Optionally rethrow the exception or handle it as needed
                throw;
            }
        }

    }
}
