using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
using DAOs.DTOs.Response;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services
{
    public interface IOrderService
    {
        public Task<List<TblOrder>> GetOrders();
        public TblOrder AddOrder(TblOrder order);
        public List<TblOrder> GetOrdersByCustomerID(int customerID);
        public List<OrderInfo> GetOrderHistory(string username);
        public TblOrder getOrderByOrderID(int orderID);
        public OrderInfo GetOrderInfo(int orderID);
        Task<bool> UpdateOrderStatus(OrderStatusRequest request);
        public Task<List<OrderInfo>> GetOrderInfoListForSaleStaff();
        public Task<List<OrderInfo>> GetOrderInforListForShipper();
        Task<bool> UpdateOrder(TblOrder order);
      
        public Task<OrderStatusCount> GetOrderStatusCountAsync();
        public Task<List<decimal>> GetRevenuePerMonthOfYear(int year);
        public Task<List<int>> GetNumberOrdersPerMonthOfYear(int year);
        public Task<decimal> GetTotalRevenueAsync();
        
    }
}
