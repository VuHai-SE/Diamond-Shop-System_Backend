using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Response;

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
        Task<List<TblOrder>> GetDeliveredOrdersByMonthAndYearAsync(int month, int year);
        public Task<OrderStatusCount> GetOrderStatusCountAsync();
    }
}
