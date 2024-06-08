using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IOrderService
    {
        public TblOrder AddOrder(TblOrder order);
        public List<TblOrder> getOrderByCustomerID(string customerID);
    }
}
