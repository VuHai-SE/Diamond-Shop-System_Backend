using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class ShipperDAO
    {
        private readonly DiamondStoreContext _dbContext;

        public ShipperDAO(DiamondStoreContext dbContext)
        {
           _dbContext = dbContext;
        }

        public TblShipper GetShipperByUsername(string username)
        {
            var acc = _dbContext.TblAccounts.FirstOrDefault(a => a.Username.Equals(username));
            return _dbContext.TblShippers.FirstOrDefault(sh => sh.AccountId.Equals(acc.AccountId));
        }

        public bool IsShipperIdExist(string shipperId)
            => _dbContext.TblShippers.Any(s => s.ShipperId.Equals(shipperId));

        public async Task AddShipperAsync(TblShipper shipper)
        {
            var existingEntity = await _dbContext.TblShippers.AsNoTracking().FirstOrDefaultAsync(s => s.ShipperId == shipper.ShipperId);
            if (existingEntity == null)
            {
                await _dbContext.TblShippers.AddAsync(shipper);
            }
            else
            {
                _dbContext.Entry(shipper).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public string GetLastShipperId()
        {
            
            var list = _dbContext.TblShippers.ToList();

            
            if (list.Count == 0) return "SP000";

            
            var lastShipper = list.OrderByDescending(s => s.ShipperId).FirstOrDefault().ShipperId;

            return lastShipper;
        }

        public List<TblShipper> GetAllShippers()
        {
            return _dbContext.TblShippers.ToList();
        }
    }
}
