using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services.Implement
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }
        public TblShipper GetShipperByUsername(string username)
            => _shipperRepository.GetShipperByUsername(username);

        public bool IsShipperIdExist(string shipperId)
            => _shipperRepository.IsShipperIdExist(shipperId);
    }
}
