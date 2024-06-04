using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IMaterialPriceListService
    {
        public List<TblMaterialPriceList> GetMaterialPriceLists();
        public TblMaterialPriceList GetMaterialPriceList(int id);

        public TblMaterialPriceList AddMaterialPriceList(TblMaterialPriceList materialPriceList);

        public bool UpdateMaterialPriceList(int id, TblMaterialPriceList materialPriceList);

        public bool DeleteMaterialPriceList(int id);
    }
}
