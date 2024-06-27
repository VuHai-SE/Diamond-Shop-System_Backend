using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories.Implement;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services
{
    public interface IMaterialPriceListService
    {
        public List<MaterialResponse> GetMaterialList();
        public MaterialResponse GetMaterial(string materialID);

        public TblMaterialPriceList AddMaterialPriceList(TblMaterialPriceList materialPriceList);

        public bool UpdateMaterialPriceList(int id, TblMaterialPriceList materialPriceList);

        public bool DeleteMaterialPriceList(int id);

        public TblMaterialPriceList GetMaterialPriceByMaterialID(string materialID);

        Task<string> CreateMaterialAsync(CreateMaterialRequest request);

        // Method to check if a MaterialId exists
        public bool IsMaterialIdExists(string materialId);

        // Method to check if a MaterialId exists in TblProductMaterial
        public bool IsMaterialIdInProductMaterial(string materialId);

        // Method to delete a Material
        public bool DeleteMaterial(string materialId);
    }
}
