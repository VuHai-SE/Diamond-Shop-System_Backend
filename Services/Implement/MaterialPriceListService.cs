using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Services.DTOs.Request;
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

        public async Task<string> CreateMaterialAsync(CreateMaterialRequest request)
        {
            if (materialPriceListRepository.IsMaterialIdExists(request.MaterialId))
            {
                return "Material ID already exists.";
            }

            if (materialPriceListRepository.IsMaterialNameExists(request.MaterialName))
            {
                return "Material Name already exists.";
            }

            var currentDate = DateTime.UtcNow;
            if (request.EffDate > currentDate.AddHours(48) || request.EffDate < currentDate.AddMonths(-6))
            {
                return "Effective date must be within the last 6 months and not more than 48 hours in the future.";
            }

            var materialCategory = new TblMaterialCategory
            {
                MaterialId = request.MaterialId,
                MaterialName = request.MaterialName
            };

            var materialPriceList = new TblMaterialPriceList
            {
                MaterialId = request.MaterialId,
                UnitPrice = request.UnitPrice,
                EffDate = request.EffDate
            };

            materialCategoryRepository.AddMaterialCategory(materialCategory);
            materialPriceListRepository.AddMaterialPriceList(materialPriceList);

            return "Material created successfully.";
        }

        // Method to check if a MaterialId exists
        public bool IsMaterialIdExists(string materialId)
            => materialPriceListRepository.IsMaterialIdExists(materialId);

        // Method to check if a MaterialId exists in TblProductMaterial
        public bool IsMaterialIdInProductMaterial(string materialId)
            => materialPriceListRepository.IsMaterialIdInProductMaterial(materialId);

        // Method to delete a Material
        public bool DeleteMaterial(string materialId)
            => materialPriceListRepository.DeleteMaterial(materialId);
    }
}
