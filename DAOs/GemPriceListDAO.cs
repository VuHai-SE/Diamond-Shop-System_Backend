using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace DAOs
{
    public class GemPriceListDAO
    {
        private readonly DiamondStoreContext dbContext;

        public GemPriceListDAO(DiamondStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public List<TblGemPriceList> GetGemPriceLists()
            => dbContext.TblGemPriceLists.ToList();

        public TblGemPriceList GetGemPriceList(int id)
            => dbContext.TblGemPriceLists.FirstOrDefault(m => m.Id.Equals(id));

        public TblGemPriceList AddGemPriceList(TblGemPriceList gemPriceList)
        {
            dbContext.TblGemPriceLists.Add(gemPriceList);
            dbContext.SaveChanges();
            return gemPriceList;
        }

        public bool UpdateGemPriceList(int id, TblGemPriceList gemPriceList)
        {
            TblGemPriceList oGemPriceList = GetGemPriceList(id);
            if (oGemPriceList != null)
            {
                oGemPriceList.Origin = gemPriceList.Origin;
                oGemPriceList.CaratWeight = gemPriceList.CaratWeight;
                oGemPriceList.Color = gemPriceList.Color;
                oGemPriceList.Cut = gemPriceList.Cut;
                oGemPriceList.Clarity = gemPriceList.Clarity;
                oGemPriceList.Price = gemPriceList.Price;
                oGemPriceList.EffDate = gemPriceList.EffDate;
                oGemPriceList.Size = gemPriceList.Size;
                dbContext.Update(oGemPriceList);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteGemPriceList(int id)
        {
            TblGemPriceList oGemPriceList = GetGemPriceList(id);
            if (oGemPriceList != null)
            {
                dbContext.Remove(oGemPriceList);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<TblGemPriceList> GetListByFourCAndOrigin(string origin, double? minCaratWeight, double? maxCaratWeght,
     string color, string cut, string clarity)
        {
            var query = dbContext.TblGemPriceLists.AsQueryable();

            if (!string.IsNullOrEmpty(origin))
            {
                var originBool = origin.Trim().Equals("Natural", StringComparison.OrdinalIgnoreCase);
                query = query.Where(g => g.Origin == originBool);
            }

            if (minCaratWeight.HasValue)
            {
                query = query.Where(g => g.CaratWeight >= minCaratWeight.Value);
            }

            if (maxCaratWeght.HasValue)
            {
                query = query.Where(g => g.CaratWeight <= maxCaratWeght.Value);
            }

            if (!string.IsNullOrEmpty(color))
            {
                color = color.Trim();
                query = query.Where(g => g.Color == color);
            }

            if (!string.IsNullOrEmpty(cut))
            {   
                cut = cut.Trim();
                query = query.Where(g => g.Cut == cut);
            }

            if (!string.IsNullOrEmpty(clarity))
            {
                clarity = clarity.Trim();
                query = query.Where(g => g.Clarity == clarity);
            }

            return query.AsNoTracking().ToList();
        }
    }
}
