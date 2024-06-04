using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class MaterialPriceListDAO
    {
        private readonly DiamondStoreContext dbContext = null;

        public MaterialPriceListDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public List<TblMaterialPriceList> GetMaterialPriceLists()
            => dbContext.TblMaterialPriceLists.ToList();

        public TblMaterialPriceList GetMaterialPriceList(int id)
            => dbContext.TblMaterialPriceLists.FirstOrDefault(m => m.Id.Equals(id));

        public TblMaterialPriceList AddMaterialPriceList(TblMaterialPriceList materialPriceList)
        {
            dbContext.TblMaterialPriceLists.Add(materialPriceList);
            dbContext.SaveChanges();
            return materialPriceList;
        }

        public bool UpdateMaterialPriceList(int id,  TblMaterialPriceList materialPriceList)
        {
            return false;
        }

        public bool DeleteMaterialPriceList(int id)
        {
            return false;
        }
    }
}
