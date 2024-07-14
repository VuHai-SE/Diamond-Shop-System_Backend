using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
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
        public async Task<List<TblOrder>> GetDeliveredOrdersByMonthAndYearAsync(int month, int year)
        {
            return await orderDAO.GetDeliveredOrdersByMonthAndYearAsync(month, year);
        }

        public async Task<OrderStatusCount> GetOrderStatusCountAsync()
            => await orderDAO.GetOrderStatusCountAsync();
    }
}
