using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class PaymentDAO
    {
        private readonly DiamondStoreContext dbContext = null;

        public PaymentDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public TblPayment AddPayment(TblPayment payment)
        {
            dbContext.TblPayments.Add(payment);
            dbContext.SaveChanges();
            return payment;
        }

        public TblPayment GetPaymentByCustomerAndOrder(int orderID, int customerID)
            => dbContext.TblPayments.FirstOrDefault(p => p.OrderId.Equals(orderID) && p.CustomerId.Equals(customerID));

        public List<TblPayment> GetPaymentsByCustomerID(int customerID)
            => dbContext.TblPayments.Where(p => p.CustomerId == customerID).ToList();
    }
}
