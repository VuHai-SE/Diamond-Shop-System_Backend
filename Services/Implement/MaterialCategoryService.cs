using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class MaterialCategoryService : IMaterialCategoryService
    {
        private readonly IMaterialCategoryRepository materialCategoryRepository;

        public MaterialCategoryService(IMaterialCategoryRepository _materialCategoryRepository)
        {
            materialCategoryRepository = _materialCategoryRepository;
        }

        public TblMaterialCategory AddMaterialCategory(TblMaterialCategory category)
            => materialCategoryRepository.AddMaterialCategory(category);

        public bool DeleteMaterialCategory(string id)
            => materialCategoryRepository.DeleteMaterialCategory(id);

        public List<TblMaterialCategory> GetMaterialCategories()
            => materialCategoryRepository.GetMaterialCategories();

        public TblMaterialCategory GetMaterialCategory(string id)
            => materialCategoryRepository.GetMaterialCategory(id);

        public bool UpdateMaterialCategory(string id, TblMaterialCategory category)
            => materialCategoryRepository.UpdateMaterialCategory(id, category);
    }
}
