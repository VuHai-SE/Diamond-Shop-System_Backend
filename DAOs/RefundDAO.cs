using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DAOs
{
    public class RefundDAO
    {
        private readonly DiamondStoreContext _context;
        public RefundDAO(DiamondStoreContext context)
        {
           _context = context;
        }

        public async Task<TblRefund> MakeRefund(TblRefund refund)
        {
            await _context.TblRefunds.AddAsync(refund);
            await _context.SaveChangesAsync();
            return refund;
        }

        public async Task<TblRefund> GetRefundById(int refundId)
        {
            return await _context.TblRefunds.FirstOrDefaultAsync(r => r.RefundId == refundId);
        }

        public async Task<TblRefund> GetRefundByPaymentId(int paymentId)
        {
            return await _context.TblRefunds.FirstOrDefaultAsync(r => r.PaymentId == paymentId);
        }

        public async Task<TblRefund> UpdateRefund(TblRefund refund)
        {
            _context.TblRefunds.Update(refund); 
            await _context.SaveChangesAsync(); 
            return refund;
        }

    }
}
