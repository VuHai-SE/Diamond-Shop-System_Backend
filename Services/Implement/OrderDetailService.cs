using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository _orderDetailRepository)
        {
            orderDetailRepository = _orderDetailRepository;
        }

        public TblOrderDetail AddOrderDetail(TblOrderDetail orderDetail)
            => orderDetailRepository.AddOrderDetail(orderDetail);

        public TblOrderDetail GetOrderDetailByID(int orderDetailID)
            => orderDetailRepository.GetOrderDetailByID(orderDetailID);

        public List<TblOrderDetail> GetOrderDetailsByOrderID(int orderID)
            => orderDetailRepository.GetOrderDetailsByOrderID(orderID);
    }
}
