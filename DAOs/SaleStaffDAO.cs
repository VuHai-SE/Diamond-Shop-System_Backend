using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;

namespace DAOs
{
    public class SaleStaffDAO
    {
        private readonly DiamondStoreContext _dbContext;

        public SaleStaffDAO(DiamondStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TblSaleStaff GetSaleStaffByUsername(string username)
        {
            var acc = _dbContext.TblAccounts.FirstOrDefault(a => a.Username.Equals(username));
            return _dbContext.TblSaleStaffs.FirstOrDefault(s => s.AccountId.Equals(acc.AccountId));
        }

        public bool isSaleStaffIdExist(string staffId)

            => _dbContext.TblSaleStaffs.Any(s => s.StaffId.Equals(staffId));

        public async Task AddSaleStaffAsync(TblSaleStaff saleStaff)
        {
            var existingEntity = await _dbContext.TblSaleStaffs.AsNoTracking().FirstOrDefaultAsync(s => s.StaffId == saleStaff.StaffId);
            if (existingEntity == null)
            {
                await _dbContext.TblSaleStaffs.AddAsync(saleStaff);
            }
            else
            {
                _dbContext.Entry(saleStaff).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }
   
        public string GetLastStaffId()
        {
            // Lấy danh sách các sale staff từ database
            var list = _dbContext.TblSaleStaffs.ToList();

            // Nếu danh sách trống, trả về "S000"
            if (list.Count == 0) return "S000";

            // Lấy staff ID cuối cùng từ danh sách đã sắp xếp
            var lastStaffId = list.OrderByDescending(s => s.StaffId).FirstOrDefault().StaffId;

            return lastStaffId;
        }
        
        public List<TblSaleStaff> GetAllSaleStaffs()
        {
            return _dbContext.TblSaleStaffs.ToList();
        }
    }
    
}
