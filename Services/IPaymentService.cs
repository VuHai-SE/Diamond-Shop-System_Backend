using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IPaymentService
    {
        public TblPayment AddPayment(TblPayment payment);
        public Task<TblPayment> GetPaymentByOrderId(int orderId);
        public List<TblPayment> GetPaymentsByCustomerID(int customerID);
    }
}
