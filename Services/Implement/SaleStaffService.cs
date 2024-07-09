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

        public Task<TblSaleStaff> GetSaleStaffByUsernameAsync(string username)
            => _saleStaffRepository.GetSaleStaffByUsernameAsync(username);

        public Task<bool> IsSaleStaffIdExistAsync(string staffId)
            => _saleStaffRepository.IsSaleStaffIdExistAsync(staffId);

    }
}
