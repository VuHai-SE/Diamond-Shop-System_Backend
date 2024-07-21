using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IPaymentRepository
    {
        public TblPayment AddPayment(TblPayment payment);
        public Task<TblPayment> GetPaymentByOrderId(int orderId);
        public List<TblPayment> GetPaymentsByCustomerID(int customerID);
    }
}
