using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class GemPriceListDAO
    {
        private readonly DiamondStoreContext dbContext = null;

        public GemPriceListDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
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
    }
}
