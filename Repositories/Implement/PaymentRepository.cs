using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Implement
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDAO paymentDAO;

        public PaymentRepository(PaymentDAO _paymentDAO)
        {
            paymentDAO = _paymentDAO;
        }

        public TblPayment AddPayment(TblPayment payment)
            => paymentDAO.AddPayment(payment);

        public async Task<TblPayment> GetPaymentByOrderId(int orderId)
           => await paymentDAO.GetPaymentByOrderId(orderId);

        public List<TblPayment> GetPaymentsByCustomerID(int customerID)
            => paymentDAO.GetPaymentsByCustomerID(customerID);
    }
}
