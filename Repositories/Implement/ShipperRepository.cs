using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly ShipperDAO _shipperDAO;

        public ShipperRepository(ShipperDAO shipperDAO)
        {
            _shipperDAO = shipperDAO;
        }

        public TblShipper GetShipperByUsername(string username)
            => _shipperDAO.GetShipperByUsername(username);

        public bool IsShipperIdExist(string shipperId)
            => _shipperDAO.IsShipperIdExist(shipperId);
    }
}
