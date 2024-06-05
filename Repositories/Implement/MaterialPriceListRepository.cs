using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class MaterialPriceListRepository : IMaterialPriceListRepository
    {
        private readonly MaterialPriceListDAO materialPriceListDAO = null;

        public MaterialPriceListRepository()
        {
            if (materialPriceListDAO == null)
            {
                materialPriceListDAO = new MaterialPriceListDAO();
            }
        }

        public TblMaterialPriceList AddMaterialPriceList(TblMaterialPriceList materialPriceList)
            => materialPriceListDAO.AddMaterialPriceList(materialPriceList);

        public bool DeleteMaterialPriceList(int id)
            => materialPriceListDAO.DeleteMaterialPriceList(id);

        public TblMaterialPriceList GetMaterialPriceList(int id)
            => materialPriceListDAO.GetMaterialPriceList(id);

        public List<TblMaterialPriceList> GetMaterialPriceLists()
            => materialPriceListDAO.GetMaterialPriceLists();

        public bool UpdateMaterialPriceList(int id, TblMaterialPriceList materialPriceList)
           => materialPriceListDAO.UpdateMaterialPriceList(id, materialPriceList);
    }
}
