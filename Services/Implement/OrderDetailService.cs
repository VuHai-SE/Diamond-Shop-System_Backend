﻿using System;
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
        private readonly IOrderDetailRepository orderDetailRepository = null;

        public OrderDetailService()
        {
            if (orderDetailRepository == null)
            {
                orderDetailRepository = new OrderDetailRepository();
            }
        }

        public TblOrderDetail AddOrderDetail(TblOrderDetail orderDetail)
            => orderDetailRepository.AddOrderDetail(orderDetail);

        public List<TblOrderDetail> GetOrderDetailsByOrderID(int orderID)
            => orderDetailRepository.GetOrderDetailsByOrderID(orderID);
    }
}
