using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services.Implement
{
    public class WarrantyService : IWarrantyService
    {
        private readonly IWarrantyRepository _warrantyRepository;

        public WarrantyService(IWarrantyRepository warrantyRepository)
        {
            _warrantyRepository = warrantyRepository;
        }

        public TblWarranty AddWarranty(TblWarranty warranty)
            => _warrantyRepository.AddWarranty(warranty);

        public List<TblWarranty> GetWarranties()
            => _warrantyRepository.GetWarranties();

        public TblWarranty GetWarrantyByID(int id)
            => _warrantyRepository.GetWarrantyByID(id);

        public TblWarranty GetWarrantyOrderDetailID(int orderDetailID)
            => _warrantyRepository.GetWarrantyOrderDetailID(orderDetailID);

        public void SaveWarrantyPdf(int warrantyId, byte[] bytes)
        {
            _warrantyRepository.SaveWarrantyPdf(warrantyId, bytes);
        }
    }
}
