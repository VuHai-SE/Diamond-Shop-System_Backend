using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories
{
    public interface IRefundRepository
    {
        public Task<TblRefund> MakeRefund(TblRefund refund);

        public Task<TblRefund> GetRefundById(int refundId);

        public Task<TblRefund> GetRefundByPaymentId(int paymentId);

        public Task<TblRefund> UpdateRefund(TblRefund refund);
    }
}
