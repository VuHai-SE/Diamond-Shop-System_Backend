using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class WarrantyDAO
    {
        private readonly DiamondStoreContext _context;

        public WarrantyDAO(DiamondStoreContext context)
        {
            _context = context;
        }

        public List<TblWarranty> GetWarranties()
            => _context.TblWarranties.ToList();

        public TblWarranty GetWarrantyByID(int id)
            => _context.TblWarranties.FirstOrDefault(w => w.WarrantyId.Equals(id));

        public TblWarranty GetWarrantyOrderDetailID(int orderDetailID)
            => _context.TblWarranties.FirstOrDefault(w => w.OrderDetailId.Equals(orderDetailID));

        public TblWarranty AddWarranty(TblWarranty warranty)
        {
            _context.TblWarranties.Add(warranty);
            _context.SaveChanges();
            return warranty;
        }

        public void UpdateWarranty(TblWarranty warranty)
        {
            _context.TblWarranties.Update(warranty);
            _context.SaveChanges();
        }
    }
}
