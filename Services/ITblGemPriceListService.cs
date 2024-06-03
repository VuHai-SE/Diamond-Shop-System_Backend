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
        public List<TblGemPriceList> GetGemPriceLists();
        public TblGemPriceList GetTblGemPriceList(int id);
        public TblGemPriceList AddTblGemPriceList(TblGemPriceList tblGemPriceList);
        public bool UpdateTblGemPriceList(int id, TblGemPriceList tblGemPriceList);
        public bool DeleteTblGemPriceList(int id);
    }
}
