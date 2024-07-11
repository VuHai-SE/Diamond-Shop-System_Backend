using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories
{
    public interface IShipperRepository
    {
        public TblShipper GetShipperByUsername(string username);
        public bool IsShipperIdExist(string shipperId);
        Task AddShipperAsync(TblShipper shipper);
        public string GetLastShipperId();
        public List<TblShipper> GetAllShippers();
    }
}
