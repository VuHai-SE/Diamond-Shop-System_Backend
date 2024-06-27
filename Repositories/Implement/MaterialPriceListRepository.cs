using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class MaterialPriceListRepository : IMaterialPriceListRepository
    {
        private readonly MaterialPriceListDAO materialPriceListDAO;

        public MaterialPriceListRepository(MaterialPriceListDAO _materialPriceListDAO)
        {
            materialPriceListDAO = _materialPriceListDAO;
        }

        public TblMaterialPriceList AddMaterialPriceList(TblMaterialPriceList materialPriceList)
            => materialPriceListDAO.AddMaterialPriceList(materialPriceList);

        public bool DeleteMaterialPriceList(int id)
            => materialPriceListDAO.DeleteMaterialPriceList(id);
                
        public TblMaterialPriceList GetMaterialPriceByMaterialID(string materialID)
            => materialPriceListDAO.GetMaterialPriceByMaterialID(materialID);

        public TblMaterialPriceList GetMaterialPriceList(int id)
            => materialPriceListDAO.GetMaterialPriceList(id);

        public List<TblMaterialPriceList> GetMaterialPriceLists()
            => materialPriceListDAO.GetMaterialPriceLists();

        public bool UpdateMaterialPriceList(int id, TblMaterialPriceList materialPriceList)
           => materialPriceListDAO.UpdateMaterialPriceList(id, materialPriceList);

        // Method to check if a MaterialId exists
        public bool IsMaterialIdExists(string materialId)
        => materialPriceListDAO.IsMaterialIdExists(materialId);

        // Method to check if a MateriaName exists
        public bool IsMaterialNameExists(string materialName)
            => materialPriceListDAO.IsMaterialNameExists(materialName);

        // Method to check if a MaterialId exists in TblProductMaterial
        public bool IsMaterialIdInProductMaterial(string materialId)
            => materialPriceListDAO.IsMaterialIdInProductMaterial(materialId);

        // Method to delete a Material
        public bool DeleteMaterial(string materialId)
            => materialPriceListDAO.DeleteMaterial(materialId);
    }
}
