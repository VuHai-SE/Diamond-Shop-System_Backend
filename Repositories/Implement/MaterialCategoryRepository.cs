using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class MaterialCategoryRepository : IMaterialCategoryRepository
    {
        private readonly MaterialCategoryDAO materialCategoryDAO = null;

        public MaterialCategoryRepository()
        {
            if (materialCategoryDAO == null)
            {
                materialCategoryDAO = new MaterialCategoryDAO();
            }
        }

        public bool DeleteMaterialCategory(string id)
            => materialCategoryDAO.DeleteMaterialCategory(id);

        public List<TblMaterialCategory> GetMaterialCategories()
            => materialCategoryDAO.GetMaterialCategories();

        public TblMaterialCategory GetMaterialCategory(string id)
            => materialCategoryDAO.GetMaterialCategory(id);

        public TblMaterialCategory AddMaterialCategory(TblMaterialCategory category)
            => materialCategoryDAO.AddMaterialCategory(category);

        public bool UpdateMaterialCategory(string id, TblMaterialCategory category)
            => materialCategoryDAO.UpdateMaterialCategory(id, category);
    }
}
