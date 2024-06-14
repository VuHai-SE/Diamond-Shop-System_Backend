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

        public TblSaleStaff GetSaleStaffByUsername(string username)
            => _saleStaffDAO.GetSaleStaffByUsername(username);
    }
}
