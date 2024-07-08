using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services.Implement
{
    public class SaleStaffService : ISaleStaffService
    {
        private readonly ISaleStaffRepository _saleStaffRepository;

        public SaleStaffService(ISaleStaffRepository saleStaffRepository)
        {
            _saleStaffRepository = saleStaffRepository;
        }

        public TblSaleStaff GetSaleStaffByUsername(string username)
            => _saleStaffRepository.GetSaleStaffByUsername(username);

        public bool isSaleStaffIdExist(string staffId)
            => _saleStaffRepository.isSaleStaffIdExist(staffId);
    }
}
