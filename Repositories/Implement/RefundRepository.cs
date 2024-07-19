using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class RefundRepository : IRefundRepository
    {
        private readonly RefundDAO _refundDAO;
        public RefundRepository(RefundDAO refundDAO)
        {
            _refundDAO = refundDAO;
        }

        public async Task<TblRefund> GetRefundById(int refundId)
            => await _refundDAO.GetRefundById(refundId);

        public async Task<TblRefund> GetRefundByPaymentId(int paymentId)
            => await GetRefundById(paymentId);

        public async Task<TblRefund> MakeRefund(TblRefund refund)
            => await _refundDAO.MakeRefund(refund);

        public async Task<TblRefund> UpdateRefund(TblRefund refund)
            => await _refundDAO.UpdateRefund(refund);
    }
}
