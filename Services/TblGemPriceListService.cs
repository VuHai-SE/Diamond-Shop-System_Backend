using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class TblGemPriceListService : ITblGemPriceListService
    {
        private readonly ITblGemPriceListRepo tblGemPriceListRepo = null;

        public TblGemPriceListService()
        {
            if (tblGemPriceListRepo == null)
            {
                tblGemPriceListRepo = new TblGemPriceListRepo();
            }
        }

        public TblMaterialCategory AddTblGemPriceList(TblMaterialCategory tblGemPriceList)
            => tblGemPriceListRepo.AddTblGemPriceList(tblGemPriceList);

        public bool DeleteTblGemPriceList(int id)
            => tblGemPriceListRepo.DeleteTblGemPriceList(id);

        public List<TblMaterialCategory> GetGemPriceLists()
            => tblGemPriceListRepo.GetGemPriceLists();

        public TblMaterialCategory GetTblGemPriceList(int id)
            => tblGemPriceListRepo.GetTblGemPriceList(id);

        public bool UpdateTblGemPriceList(int id, TblMaterialCategory tblGemPriceList)
            => tblGemPriceListRepo.UpdateTblGemPriceList(id, tblGemPriceList);
    }
}
