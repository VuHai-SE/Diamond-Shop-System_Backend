using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IMaterialPriceListRepository
    {
        public List<TblMaterialPriceList> GetMaterialPriceLists();

        public TblMaterialPriceList GetMaterialPriceList(int id);

        public TblMaterialPriceList AddMaterialPriceList(TblMaterialPriceList materialPriceList);

        public bool UpdateMaterialPriceList(int id, TblMaterialPriceList materialPriceList);

        public bool DeleteMaterialPriceList(int id);

        public TblMaterialPriceList GetMaterialPriceByMaterialID(string materialID);

        // Method to check if a MaterialId exists
        public bool IsMaterialIdExists(string materialId);

        // Method to check if a MaterialName exists
        public bool IsMaterialNameExists(string materialName);

        // Method to check if a MaterialId exists in TblProductMaterial
        public bool IsMaterialIdInProductMaterial(string materialId);

        // Method to delete a Material
        public bool DeleteMaterial(string materialId);
    }
}
