using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IGemPriceListService
    {
        public List<TblGemPriceList> GetGemPriceLists();

        public TblGemPriceList GetGemPriceList(int id);

        public TblGemPriceList AddGemPriceList(TblGemPriceList gemPriceList);

        public bool UpdateGemPriceList(int id, TblGemPriceList gemPriceList);

        public bool DeleteGemPriceList(int id);
    }
}
