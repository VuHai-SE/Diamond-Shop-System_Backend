using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class SaleStaffRepository : ISaleStaffRepository
    {
        private readonly SaleStaffDAO _saleStaffDAO;
        
        public SaleStaffRepository(SaleStaffDAO saleStaffDAO)
        {
            _saleStaffDAO = saleStaffDAO;
        }

        public Task<TblSaleStaff> GetSaleStaffByUsernameAsync(string username)
            => _saleStaffDAO.GetSaleStaffByUsernameAsync(username);
        public Task<bool> IsSaleStaffIdExistAsync(string staffId)
            => _saleStaffDAO.IsSaleStaffIdExistAsync(staffId);
    }
}
