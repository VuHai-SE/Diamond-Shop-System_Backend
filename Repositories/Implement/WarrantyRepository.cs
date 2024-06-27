using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class WarrantyRepository : IWarrantyRepository
    {
        private readonly WarrantyDAO _warrantyDAO;

        public WarrantyRepository(WarrantyDAO warrantyDAO)
        {
            _warrantyDAO = warrantyDAO;
        }

        public TblWarranty AddWarranty(TblWarranty warranty)
            => _warrantyDAO.AddWarranty(warranty);

        public List<TblWarranty> GetWarranties()
            => _warrantyDAO.GetWarranties();

        public TblWarranty GetWarrantyByID(int id)
            => _warrantyDAO.GetWarrantyByID(id);

        public TblWarranty GetWarrantyOrderDetailID(int orderDetailID)
            => _warrantyDAO.GetWarrantyOrderDetailID(orderDetailID);

        public void SaveWarrantyImg(int warrantyId, byte[] bytes)
        {
            var warranty = _warrantyDAO.GetWarrantyByID(warrantyId);
            if (warranty != null)
            {
                warranty.WarrantyPdf = bytes;
                _warrantyDAO.UpdateWarranty(warranty);
            }
        }
    }
}
