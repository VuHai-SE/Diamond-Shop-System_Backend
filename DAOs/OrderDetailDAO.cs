using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class OrderDetailDAO
    {
        private readonly DiamondStoreContext dbContext;

        public OrderDetailDAO(DiamondStoreContext _dbcontext)
        {
            dbContext = _dbcontext;
        }

        public TblOrderDetail AddOrderDetail(TblOrderDetail orderDetail)
        {

            dbContext.TblOrderDetails.Add(orderDetail);
            dbContext.SaveChanges();
            return orderDetail;
        }

        public List<TblOrderDetail> GetOrderDetailsByOrderID(int orderID)
        {
            if (orderID > 0)
            {
                return dbContext.TblOrderDetails.Where(od => od.OrderId.Equals(orderID)).ToList();
            }
            return new List<TblOrderDetail>();
        }

        public TblOrderDetail GetOrderDetailByID(int orderDetailID)
            => dbContext.TblOrderDetails.FirstOrDefault(o => o.OrderDetailId.Equals(orderDetailID));
    }
}
