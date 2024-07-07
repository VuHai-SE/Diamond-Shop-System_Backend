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
        private readonly DiamondStoreContext dbContext;

        public MaterialPriceListDAO(DiamondStoreContext _dbContext)
        {
            dbContext = _dbContext;
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

        // Method to check if a MaterialId exists
        public bool IsMaterialIdExists(string materialId)
        => dbContext.TblMaterialCategories.Any(mc => mc.MaterialId.Equals(materialId));

        // Method to check if a MaterialName exists
        public bool IsMaterialNameExists(string materialName)
            => dbContext.TblMaterialCategories.Any(mc => mc.MaterialName.Equals(materialName));

        // Method to check if a MaterialId exists in TblProductMaterial
        public bool IsMaterialIdInProductMaterial(string materialId)
            => dbContext.TblProductMaterials.Any(pm => pm.MaterialId.Equals(materialId));

        // Method to delete a Material
        public bool DeleteMaterial(string materialId)
        {
            var material = dbContext.TblMaterialCategories.FirstOrDefault(mc => mc.MaterialId.Equals(materialId));
            if (material == null)
            {
                return false;
            }

            var materialPriceList = dbContext.TblMaterialPriceLists.Where(mp => mp.MaterialId.Equals(materialId)).ToList();
            dbContext.TblMaterialPriceLists.RemoveRange(materialPriceList);
            dbContext.TblMaterialCategories.Remove(material);
            dbContext.SaveChanges();
            return true;
        }
    }
}
