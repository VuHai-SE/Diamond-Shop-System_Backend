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
        public TblOrder AddOrder(TblOrder order);
        public List<TblOrder> getOrderByCustomerID(int customerID);
    }
}
