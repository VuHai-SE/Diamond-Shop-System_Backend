using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IWarrantyService
    {
        public List<TblWarranty> GetWarranties();
        public TblWarranty GetWarrantyByID(int id);

        public TblWarranty GetWarrantyOrderDetailID(int orderDetailID);

        public TblWarranty AddWarranty(TblWarranty warranty);
        public void SaveWarrantyPdf(int warrantyId, byte[] bytes);
    }
}
