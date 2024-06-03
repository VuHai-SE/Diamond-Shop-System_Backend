using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories
{
    public class GemPriceListRepository : IGemPriceListRepository
    {
        private readonly GemPriceListDAO gemPriceListDAO = null;

        public GemPriceListRepository()
        {
            if (gemPriceListDAO == null)
            {
                gemPriceListDAO= new GemPriceListDAO();
            }
        }

        public TblGemPriceList AddGemPriceList(TblGemPriceList gemPriceList)
            => gemPriceListDAO.AddGemPriceList(gemPriceList);

        public bool DeleteGemPriceList(int id)
            => gemPriceListDAO.DeleteGemPriceList(id);

        public TblGemPriceList GetGemPriceList(int id)
            => gemPriceListDAO.GetGemPriceList(id);

        public List<TblGemPriceList> GetGemPriceLists()
            => gemPriceListDAO.GetGemPriceLists();

        public bool UpdateGemPriceList(int id, TblGemPriceList gemPriceList)
            => gemPriceListDAO.UpdateGemPriceList(id, gemPriceList);
    }
}
