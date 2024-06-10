using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {

        private readonly OrderDAO orderDAO = null;

        public OrderRepository()
        {
            if (orderDAO == null)
            {
                orderDAO = new OrderDAO();
            }
        }
        public TblOrder AddOrder(TblOrder order)
            => orderDAO.AddOrder(order);

        public List<TblOrder> getOrderByCustomerID(int customerID)
            => orderDAO.getOrderByCustomerID(customerID);
    }
}
