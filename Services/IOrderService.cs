using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services
{
    public interface IOrderService
    {
        public List<TblOrder> GetOrders();
        public TblOrder AddOrder(TblOrder order);
        public List<TblOrder> GetOrdersByCustomerID(int customerID);
        public List<OrderInfo> GetOrderHistory(string username);
        public TblOrder getOrderByOrderID(int orderID);
        public void CancelOrder(int orderID);
        public OrderInfo GetOrderInfo(int orderID);
        Task<bool> UpdateOrderStatus(OrderStatusRequest request);
        public List<OrderInfo> GetOrderInfoListForSaleStaff();
        public List<OrderInfo> GetOrderInforListForShipper();
    }
}
