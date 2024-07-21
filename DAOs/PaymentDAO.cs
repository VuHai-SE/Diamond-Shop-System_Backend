using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class PaymentDAO
    {
        private readonly DiamondStoreContext dbContext;

        public PaymentDAO(DiamondStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public TblPayment AddPayment(TblPayment payment)
        {
            dbContext.TblPayments.Add(payment);
            dbContext.SaveChanges();
            return payment;
        }

        public async Task<TblPayment> GetPaymentByOrderId(int orderId)
            => await dbContext.TblPayments.FirstOrDefaultAsync(p => p.OrderId.Equals(orderId));

        public List<TblPayment> GetPaymentsByCustomerID(int customerID)
            => dbContext.TblPayments.Where(p => p.CustomerId == customerID).ToList();
    }
}
