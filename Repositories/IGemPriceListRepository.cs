using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IGemPriceListRepository
    {
        public List<TblGemPriceList> GetGemPriceLists();

        public TblGemPriceList GetGemPriceList(int id);

        public TblGemPriceList AddGemPriceList(TblGemPriceList gemPriceList);

        public bool UpdateGemPriceList(int id, TblGemPriceList gemPriceList);

        public bool DeleteGemPriceList(int id);

        public List<TblGemPriceList> GetListByFourCAndOrigin(string origin, double? minCaratWeight, double? maxCaratWeght,
     string color, string cut, string clarity);


    }
}
