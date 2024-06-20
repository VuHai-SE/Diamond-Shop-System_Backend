using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Implement;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services.Implement
{
    public class GemPriceListService : IGemPriceListService
    {
        private readonly IGemPriceListRepository gemPriceListRepository;

        public GemPriceListService(IGemPriceListRepository _gemPriceListRepository)
        {
            gemPriceListRepository = _gemPriceListRepository;
        }

        public TblGemPriceList AddGemPriceList(TblGemPriceList gemPriceList)
            => gemPriceListRepository.AddGemPriceList(gemPriceList);

        public bool DeleteGemPriceList(int id)
            => gemPriceListRepository.DeleteGemPriceList(id);

        public List<GemPriceResponse> GetListByFourCAndOrigin(GemPriceListFilterCriteria criteria)
        {

            var list = gemPriceListRepository.GetListByFourCAndOrigin(criteria.Origin, criteria.MinCaratWeight, criteria.MaxCaratWeight, criteria.Color, criteria.Cut, criteria.Clarity);
            var gemPriceList = new List<GemPriceResponse>();
            foreach (var g in list)
            {
                var gemPrice = GetGemPrice(g.Id);
                gemPriceList.Add(gemPrice);
            }
            return gemPriceList;
        }


        public GemPriceResponse GetGemPrice(int id)
        {
            var gemPriceList = gemPriceListRepository.GetGemPriceList(id);
            if (gemPriceList == null)
            {
                return null;
            }

            return new GemPriceResponse()
            {
                Id = gemPriceList.Id,
                Origin = (gemPriceList.Origin == true) ? "Natural" : "Synthetic",
                CaratWeight = gemPriceList.CaratWeight,
                Color = gemPriceList.Color,
                Cut = gemPriceList.Cut,
                Clarity = gemPriceList.Clarity,
                Price = gemPriceList.Price,
                EffDate = gemPriceList.EffDate
            };
        }

        public List<GemPriceResponse> GetGemPriceLists()
        {
            var list = gemPriceListRepository.GetGemPriceLists();
            var gemPriceList = new List<GemPriceResponse>();
            foreach (var g in list)
            {
                var gemPrice = GetGemPrice(g.Id);
                gemPriceList.Add(gemPrice);
            }
            return gemPriceList;
        }

        public bool UpdateGemPriceList(UpdateGemPriceRequest request)
        {
            var gemPrice = gemPriceListRepository.GetGemPriceList(request.ID);
            if (gemPrice == null)
            {
                return false;
            }

            gemPrice.Price = request.NewPrice;
            gemPrice.EffDate = request.EffectDate;
            gemPriceListRepository.UpdateGemPriceList(request.ID, gemPrice);
            return true;
        }
    }
}
