using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services.DTOs.Response;

namespace Services
{
    public interface IOrderService
    {
        public List<TblOrder> GetOrders();
        public TblOrder AddOrder(TblOrder order);
        public List<TblOrder> getOrderByCustomerID(int customerID);
        public List<OrderInfo> GetOrderHistory(int AccountID);
        public TblOrder getOrderByOrderID(int orderID);
        public void CancelOrder(int orderID);
        public OrderInfo GetOrderInfo(int orderID);
        Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
