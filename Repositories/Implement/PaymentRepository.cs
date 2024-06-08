using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDAO paymentDAO = null;

        public PaymentRepository()
        {
            if (paymentDAO == null)
            {
                paymentDAO = new PaymentDAO();
            }
        }

        public TblPayment AddPayment(TblPayment payment)
            => paymentDAO.AddPayment(payment);

        public TblPayment GetPaymentByCustomerAndOrder(int orderID, int customerID)
            => paymentDAO.GetPaymentByCustomerAndOrder(orderID, customerID);

        public List<TblPayment> GetPaymentsByCustomerID(int customerID)
            => paymentDAO.GetPaymentsByCustomerID(customerID);
    }
}
