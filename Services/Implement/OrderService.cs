using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository = null;

        public OrderService()
        {
            if (orderRepository == null)
            {
                orderRepository = new OrderRepository();
            }
        }

        public TblOrder AddOrder(TblOrder order)
            => orderRepository.AddOrder(order);

        public List<TblOrder> getOrderByCustomerID(string customerID)
            => orderRepository.getOrderByCustomerID(customerID);
    }
}
