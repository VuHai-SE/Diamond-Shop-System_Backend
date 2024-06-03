using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOS;

namespace DAOs
{
    public class TblMaterialCategoryDAO
    {
        private readonly DiamondStoreContext dbContext = null;

        public TblMaterialCategoryDAO() // kỹ thuật singleton
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public List<TblMaterialCategory> GetMaterialCategorys() => dbContext.TblMaterialCategories.ToList();

        public TblMaterialCategory GetTblMaterialCategory(int id) => dbContext.TblMaterialCategories.FirstOrDefault(m => m.Id.Equals(id));

        public TblMaterialCategory AddTblMaterialCategory(TblMaterialCategory tblMaterialCategory)
        {
            try
            {
                dbContext.TblMaterialCategories.Add(tblMaterialCategory);
                dbContext.SaveChanges();
                return tblMaterialCategory;
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool UpdateTblMaterialCategory(int id, TblMaterialCategory tblMaterialCategory)
        {
            try
            {
                TblMaterialCategory oTblMaterialCategory = GetTblMaterialCategory(id);
                if (oTblMaterialCategory != null)
                {
                    oTblMaterialCategory.MaterialId = tblMaterialCategory.MaterialId;
                    oTblMaterialCategory.MaterialName = tblMaterialCategory.MaterialName;
                    dbContext.Update(oTblMaterialCategory);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteTblMaterialCategory(int id)
        {
            try
            {
                TblMaterialCategory oTblMaterialCategory = GetTblMaterialCategory(id);
                if (oTblMaterialCategory != null)
                {
                    dbContext.Remove(oTblMaterialCategory);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
