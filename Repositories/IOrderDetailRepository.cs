using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IOrderDetailRepository
    {
        public TblOrderDetail AddOrderDetail(TblOrderDetail orderDetail);
        public List<TblOrderDetail> GetOrderDetailsByOrderID(int orderID);
        public TblOrderDetail GetOrderDetailByID(int orderDetailID);
    }
}
