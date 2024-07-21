using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
using DAOs.DTOs.Response;
using Services.DTOs.Response;

namespace Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {

        private readonly OrderDAO orderDAO;

        public OrderRepository(OrderDAO _orderDAO)
        {
           orderDAO = _orderDAO;
        }

        public TblOrder AddOrder(TblOrder order)
            => orderDAO.AddOrder(order);
        public void CancelOrder(int orderID)
           => orderDAO.CancelOrder(orderID);
        public List<TblOrder> getOrderByCustomerID(int customerID)
            => orderDAO.getOrderByCustomerID(customerID);

        public TblOrder getOrderByOrderID(int orderID)
            => orderDAO.getOrderByOrderID(orderID);

        public async Task<List<TblOrder>> GetOrders()
            => await orderDAO.GetOrders();

        public async Task<TblOrder> GetOrderById(int orderId)
        {
            return await orderDAO.GetOrderById(orderId);
        }
          
        public async Task<bool> UpdateOrder(TblOrder order)
        {
            return await orderDAO.UpdateOrder(order);
        }
        
        public async Task<OrderStatusCount> GetOrderStatusCountAsync()
            => await orderDAO.GetOrderStatusCountAsync();

        public async Task<List<int>> GetNumberOrdersPerMonthOfYear(int year)
            => await orderDAO.GetNumberOrdersPerMonthOfYear(year);
        public async Task<List<decimal>> GetRevenuePerMonthOfYear(int year)
            => await orderDAO.GetRevenuePerMonthOfYear(year);
        public async Task<decimal> GetTotalRevenueAsync()
            => await orderDAO.GetTotalRevenueAsync();
    }
}
