using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services.Implement
{
    public class ProductMaterialService : IProductMaterialService
    {
        private readonly IProductMaterialRepository _productMaterialRepository;

        public ProductMaterialService(IProductMaterialRepository productMaterialRepository)
        {
            _productMaterialRepository = productMaterialRepository;
        }

        public List<TblProductMaterial> GetProductMaterialByMaterialID(string materialID)
            => _productMaterialRepository.GetProductMaterialByMaterialID(materialID);
        public TblProductMaterial GetProductMaterialProductID(string productID)
            => _productMaterialRepository.GetProductMaterialProductID(productID);

        public List<TblProductMaterial> GetProductMaterials()
            => _productMaterialRepository.GetProductMaterials();
    }
}
