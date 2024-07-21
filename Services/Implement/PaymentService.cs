using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentService(IPaymentRepository _paymentRepository)
        {
            paymentRepository = _paymentRepository;
        }

        public TblPayment AddPayment(TblPayment payment)
            => paymentRepository.AddPayment(payment);

        public async Task<TblPayment> GetPaymentByOrderId(int orderId)
           => await paymentRepository.GetPaymentByOrderId(orderId);

        public List<TblPayment> GetPaymentsByCustomerID(int customerID)
            => paymentRepository.GetPaymentsByCustomerID(customerID);
    }
}
