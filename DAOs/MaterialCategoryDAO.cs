using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class MaterialCategoryDAO
    {
        private readonly DiamondStoreContext dbContext;

        public MaterialCategoryDAO(DiamondStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public List<TblMaterialCategory> GetMaterialCategories()
            => dbContext.TblMaterialCategories.ToList();

        public TblMaterialCategory GetMaterialCategory(string id)
            => dbContext.TblMaterialCategories.FirstOrDefault(m => m.MaterialId.Equals(id));

        public TblMaterialCategory AddMaterialCategory(TblMaterialCategory category)
        {
            dbContext.TblMaterialCategories.Add(category);
            dbContext.SaveChanges();
            return category;
        }

        public bool UpdateMaterialCategory(string id,  TblMaterialCategory category)
        {
            return false;
        }

        public bool DeleteMaterialCategory(string id)
        {
            return false;
        }
    }
}
