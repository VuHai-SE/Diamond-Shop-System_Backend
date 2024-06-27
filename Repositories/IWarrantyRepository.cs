using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IWarrantyRepository
    {
        public List<TblWarranty> GetWarranties();
        public TblWarranty GetWarrantyByID(int id);

        public TblWarranty GetWarrantyOrderDetailID(int orderDetailID);

        public TblWarranty AddWarranty(TblWarranty warranty);
        public void SaveWarrantyImg(int warrantyId, byte[] bytes);
    }
}
