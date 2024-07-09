using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface ISaleStaffService
    {
        public Task<TblSaleStaff> GetSaleStaffByUsernameAsync(string username);

        public Task<bool> IsSaleStaffIdExistAsync(string staffId);
    }
}
