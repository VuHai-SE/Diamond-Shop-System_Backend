using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories
{
    public interface ISaleStaffRepository
    {
        public TblSaleStaff GetSaleStaffByUsername(string username);
        public bool isSaleStaffIdExist(string staffId);

        Task AddSaleStaffAsync(TblSaleStaff saleStaff);
        public string GetLastStaffId();
        public List<TblSaleStaff> GetAllSaleStaffs();

    }
}
