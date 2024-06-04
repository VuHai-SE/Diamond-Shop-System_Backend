using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IMaterialCategoryRepository
    {
        public List<TblMaterialCategory> GetMaterialCategories();
        public TblMaterialCategory GetMaterialCategory(string id);
        public TblMaterialCategory AddMaterialCategory(TblMaterialCategory category);
        public bool UpdateMaterialCategory(string id, TblMaterialCategory category);
        public bool DeleteMaterialCategory(string id);
    }
}
