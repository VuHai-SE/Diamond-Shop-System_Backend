using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly OrderDetailDAO orderDetailDAO;

        public OrderDetailRepository(OrderDetailDAO _orderDetailDAO)
        {
           orderDetailDAO = _orderDetailDAO;
        }

        public TblOrderDetail AddOrderDetail(TblOrderDetail orderDetail)
            => orderDetailDAO.AddOrderDetail(orderDetail);

        public TblOrderDetail GetOrderDetailByID(int orderDetailID)
            => orderDetailDAO.GetOrderDetailByID(orderDetailID);

        public List<TblOrderDetail> GetOrderDetailsByOrderID(int orderID)
            => orderDetailDAO.GetOrderDetailsByOrderID(orderID);
    }
}
