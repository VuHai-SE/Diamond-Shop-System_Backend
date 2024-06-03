using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using DAOS;

namespace DAOs
{
    public class TblGemPriceListDAO
    {
        private readonly DiamondStoreContext dbContext = null;

        public TblGemPriceListDAO() // kỹ thuật singleton
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public List<TblMaterialCategory> GetGemPriceLists() => dbContext.TblGemPriceLists.ToList();

        public TblMaterialCategory GetTblGemPriceList(int id) => dbContext.TblGemPriceLists.FirstOrDefault(m => m.Id.Equals(id));

        public TblMaterialCategory AddTblGemPriceList(TblMaterialCategory tblGemPriceList)
        {
            try
            {
                dbContext.TblGemPriceLists.Add(tblGemPriceList);
                dbContext.SaveChanges();
                return tblGemPriceList;
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool UpdateTblGemPriceList(int id, TblMaterialCategory tblGemPriceList)
        {
            try
            {
                TblMaterialCategory oTblGemPriceList = GetTblGemPriceList(id);
                if (oTblGemPriceList != null)
                {
                    oTblGemPriceList.Origin = tblGemPriceList.Origin;
                    oTblGemPriceList.CaratWeight = tblGemPriceList.CaratWeight;
                    oTblGemPriceList.Color = tblGemPriceList.Color;
                    oTblGemPriceList.Cut = tblGemPriceList.Cut;
                    oTblGemPriceList.Clarity = tblGemPriceList.Clarity;
                    oTblGemPriceList.Price = tblGemPriceList.Price;
                    oTblGemPriceList.EffDate = tblGemPriceList.EffDate;
                    oTblGemPriceList.Size = tblGemPriceList.Size;
                    dbContext.Update(oTblGemPriceList);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteTblGemPriceList(int id)
        {
            try
            {
                TblMaterialCategory oTblGemPriceList = GetTblGemPriceList(id);
                if (oTblGemPriceList != null)
                {
                    dbContext.Remove(oTblGemPriceList);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
