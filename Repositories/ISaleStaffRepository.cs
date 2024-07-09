using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface ISaleStaffRepository
    {
        public Task<TblSaleStaff> GetSaleStaffByUsernameAsync(string username);

        public Task<bool> IsSaleStaffIdExistAsync(string staffId);
    }
}
