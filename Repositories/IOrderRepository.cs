using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Response;
using SkiaSharp;

namespace Repositories
{
    public interface IOrderRepository
    {
        public Task<List<TblOrder>> GetOrders();
        public TblOrder AddOrder(TblOrder order);
        public List<TblOrder> getOrderByCustomerID(int customerID);
        public TblOrder getOrderByOrderID(int orderID);
        public void CancelOrder(int orderID);
        Task<TblOrder> GetOrderById(int orderId);
        Task<bool> UpdateOrder(TblOrder order);
        
        public Task<OrderStatusCount> GetOrderStatusCountAsync();

        public Task<List<int>> GetNumberOrdersPerMonthOfYear(int year);

        public Task<List<decimal>> GetRevenuePerMonthOfYear(int year);
        public Task<decimal> GetTotalRevenueAsync();
    }
}
