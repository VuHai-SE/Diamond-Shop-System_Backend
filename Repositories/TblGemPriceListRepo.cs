using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories
{
    public class TblGemPriceListRepo : ITblGemPriceListRepo
    {
        public readonly TblGemPriceListDAO tblGemPriceListDAO = null;
        
        public TblGemPriceListRepo()
        {
            if (tblGemPriceListDAO == null)
            {
                tblGemPriceListDAO= new TblGemPriceListDAO();
            }
        }
        public TblMaterialCategory AddTblGemPriceList(TblMaterialCategory tblGemPriceList)
            => tblGemPriceListDAO.AddTblGemPriceList(tblGemPriceList);

        public bool DeleteTblGemPriceList(int id)
            => tblGemPriceListDAO.DeleteTblGemPriceList(id);

        public List<TblMaterialCategory> GetGemPriceLists()
            => tblGemPriceListDAO.GetGemPriceLists();

        public TblMaterialCategory GetTblGemPriceList(int id)
            => tblGemPriceListDAO.GetTblGemPriceList(id);

        public bool UpdateTblGemPriceList(int id, TblMaterialCategory tblGemPriceList)
            => tblGemPriceListDAO.UpdateTblGemPriceList(id, tblGemPriceList);
    }
}
