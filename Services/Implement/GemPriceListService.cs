using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class GemPriceListService : IGemPriceListService
    {
        private readonly IGemPriceListRepository gemPriceListRepository = null;

        public GemPriceListService()
        {
            if (gemPriceListRepository == null)
            {
                gemPriceListRepository = new GemPriceListRepository();
            }
        }

        public TblGemPriceList AddGemPriceList(TblGemPriceList gemPriceList)
            => gemPriceListRepository.AddGemPriceList(gemPriceList);

        public bool DeleteGemPriceList(int id)
            => gemPriceListRepository.DeleteGemPriceList(id);

        public TblGemPriceList GetGemPriceList(int id)
            => gemPriceListRepository.GetGemPriceList(id);

        public List<TblGemPriceList> GetGemPriceLists()
            => gemPriceListRepository.GetGemPriceLists();

        public bool UpdateGemPriceList(int id, TblGemPriceList gemPriceList)
            => gemPriceListRepository.UpdateGemPriceList(id, gemPriceList);
    }
}
