using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface ITblGemPriceListService
    {
        public List<TblMaterialCategory> GetGemPriceLists();
        public TblMaterialCategory GetTblGemPriceList(int id);
        public TblMaterialCategory AddTblGemPriceList(TblMaterialCategory tblGemPriceList);
        public bool UpdateTblGemPriceList(int id, TblMaterialCategory tblGemPriceList);
        public bool DeleteTblGemPriceList(int id);
    }
}
