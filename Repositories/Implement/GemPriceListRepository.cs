using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class GemPriceListRepository : IGemPriceListRepository
    {
        private readonly GemPriceListDAO gemPriceListDAO;

        public GemPriceListRepository(GemPriceListDAO _gemPriceListDAO)
        {
            gemPriceListDAO = _gemPriceListDAO;
        }

        public TblGemPriceList AddGemPriceList(TblGemPriceList gemPriceList)
            => gemPriceListDAO.AddGemPriceList(gemPriceList);

        public bool DeleteGemPriceList(int id)
            => gemPriceListDAO.DeleteGemPriceList(id);

        public List<TblGemPriceList> GetListByFourCAndOrigin(string origin, double? minCaratWeight, double? maxCaratWeght,
     string color, string cut, string clarity)
            => gemPriceListDAO.GetListByFourCAndOrigin(origin, minCaratWeight, maxCaratWeght, color, cut, clarity);

        public TblGemPriceList GetGemPriceList(int id)
            => gemPriceListDAO.GetGemPriceList(id);

        public List<TblGemPriceList> GetGemPriceLists()
            => gemPriceListDAO.GetGemPriceLists();

        public bool UpdateGemPriceList(int id, TblGemPriceList gemPriceList)
            => gemPriceListDAO.UpdateGemPriceList(id, gemPriceList);
    }
}
