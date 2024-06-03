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

        public TblGemPriceList AddTblGemPriceList(TblGemPriceList tblGemPriceList)
            => tblGemPriceListRepo.AddTblGemPriceList(tblGemPriceList);

        public bool DeleteTblGemPriceList(int id)
            => tblGemPriceListRepo.DeleteTblGemPriceList(id);

        public List<TblGemPriceList> GetGemPriceLists()
            => tblGemPriceListRepo.GetGemPriceLists();

        public TblGemPriceList GetTblGemPriceList(int id)
            => tblGemPriceListRepo.GetTblGemPriceList(id);

        public bool UpdateTblGemPriceList(int id, TblGemPriceList tblGemPriceList)
            => tblGemPriceListRepo.UpdateTblGemPriceList(id, tblGemPriceList);
    }
}
