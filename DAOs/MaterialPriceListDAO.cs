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
            var materialPrice = GetMaterialPriceList(id);
            if (materialPrice == null)
            {
                return false;
            }
            materialPrice.UnitPrice = materialPriceList.UnitPrice;
            materialPrice.EffDate=  materialPriceList.EffDate;
            dbContext.TblMaterialPriceLists.Update(materialPrice);
            dbContext.SaveChanges();
            return true;
        }

        public TblMaterialPriceList GetMaterialPriceByMaterialID(string materialID)
        {
            return dbContext.TblMaterialPriceLists.FirstOrDefault(mp => mp.MaterialId.Equals(materialID));
        }

        public bool DeleteMaterialPriceList(int id)
        {
            return false;
        }

        public bool IsMaterialIdExists(string materialId)
        => dbContext.TblMaterialCategories.Any(mc => mc.MaterialId.Equals(materialId));

        public bool IsMaterialNameExists(string materialName)
            => dbContext.TblMaterialCategories.Any(mc => mc.MaterialName.Equals(materialName));
    }
}
