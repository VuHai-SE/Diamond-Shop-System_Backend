using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services.DTOs.Response;

namespace Services
{
    public interface IWarrantyService
    {
        public List<TblWarranty> GetWarranties();
        public TblWarranty GetWarrantyByID(int id);

        public TblWarranty GetWarrantyOrderDetailID(int orderDetailID);

        public TblWarranty AddWarranty(TblWarranty warranty);
        public void SaveWarrantyImg(int warrantyId, byte[] bytes);
        //Task<WarrantyResponse> GetWarrantyInfo(int orderDetailID);
    }
}
