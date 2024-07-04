using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IOrderRepository
    {
        public List<TblOrder> GetOrders();
        public TblOrder AddOrder(TblOrder order);
        public List<TblOrder> getOrderByCustomerID(int customerID);
        public TblOrder getOrderByOrderID(int orderID);

        Task<TblOrder> GetOrderById(int orderId);
        Task<bool> UpdateOrder(TblOrder order);
    }
}
