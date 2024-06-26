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
        public TblPayment GetPaymentByCustomerAndOrder(int orderID, int customerID);
        public List<TblPayment> GetPaymentsByCustomerID(int customerID);
    }
}
