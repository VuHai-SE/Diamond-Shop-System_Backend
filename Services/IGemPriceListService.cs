using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services
{
    public interface IGemPriceListService
    {
        public List<GemPriceResponse> GetGemPriceLists();

        public GemPriceResponse GetGemPrice(int id);

        public TblGemPriceList AddGemPriceList(TblGemPriceList gemPriceList);

        public bool UpdateGemPriceList(UpdateGemPriceRequest request);

        public bool DeleteGemPriceList(int id);
        List<GemPriceResponse> GetListByFourCAndOrigin(GemPriceListFilterCriteria criteria);
    }
}
