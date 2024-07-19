using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
using Repositories;

namespace Services.Implement
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository _refundRepository;
        public RefundService(IRefundRepository refundRepository)
        {
            _refundRepository = refundRepository;
        }

        public async Task<TblRefund> GetRefundById(int refundId)
            => await _refundRepository.GetRefundById(refundId);

        public async Task<TblRefund> GetRefundByPaymentId(int paymentId)
            => await GetRefundById(paymentId);

        public async Task<TblRefund> MakeRefund(TblRefund refund)
            => await _refundRepository.MakeRefund(refund);

        public async Task<TblRefund> UpdateRefund(TblRefund refund)
            => await _refundRepository.UpdateRefund(refund);
    }
}
