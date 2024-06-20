using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Services.DTOs.Response;

namespace Services.Implement
{
    public class MaterialPriceListService : IMaterialPriceListService
    {
        private readonly IMaterialPriceListRepository materialPriceListRepository;
        private readonly IMaterialCategoryRepository materialCategoryRepository;

        public MaterialPriceListService(IMaterialPriceListRepository _materialPriceListRepository, IMaterialCategoryRepository _materialCategoryRepository)
        {
            if (materialPriceListRepository == null)
            {
                materialPriceListRepository = _materialPriceListRepository;
                materialCategoryRepository = _materialCategoryRepository;
            }
        }

        public MaterialResponse GetMaterial(string materialID)
        {
            var materialCategory = materialCategoryRepository.GetMaterialCategory(materialID);
            var materialPrice = materialPriceListRepository.GetMaterialPriceByMaterialID(materialID);
            if (materialPrice == null || materialPrice == null)
            {
                return null;
            }

            return new MaterialResponse()
            {
                materialID = materialCategory.MaterialId,
                materialName = materialCategory.MaterialName,
                UnitPrice = materialPrice.UnitPrice,
                EffectedDate = materialPrice.EffDate
            };
        }

        public List<MaterialResponse> GetMaterialList()
        {
            var materialCateList = materialCategoryRepository.GetMaterialCategories();
            var materialList = new List<MaterialResponse>();
            foreach ( var mc in materialCateList)
            {
                var m = GetMaterial(mc.MaterialId);
                materialList.Add(m);
            }
            return materialList;
        }

        public TblMaterialPriceList AddMaterialPriceList(TblMaterialPriceList materialPriceList)
            => materialPriceListRepository.AddMaterialPriceList(materialPriceList);

        public bool DeleteMaterialPriceList(int id)
            => materialPriceListRepository.DeleteMaterialPriceList(id);

        public TblMaterialPriceList GetMaterialPriceByMaterialID(string materialID)
            => materialPriceListRepository.GetMaterialPriceByMaterialID(materialID);

        public List<TblMaterialPriceList> GetMaterialPriceLists()
            => materialPriceListRepository.GetMaterialPriceLists();

        public bool UpdateMaterialPriceList(int id, TblMaterialPriceList materialPriceList)
            => materialPriceListRepository.UpdateMaterialPriceList(id, materialPriceList);
    }
}
