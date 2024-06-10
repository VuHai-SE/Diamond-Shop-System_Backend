using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class ProductMaterialRepository : IProductMaterialRepository
    {
        private readonly ProductMaterialDAO _productMaterialDao;

        public ProductMaterialRepository(ProductMaterialDAO productMaterialDao)
        {
            _productMaterialDao = productMaterialDao;
        }

        public TblProductMaterial GetProductMaterialProductID(string productID)
            => _productMaterialDao.GetProductMaterialProductID(productID);

        public List<TblProductMaterial> GetProductMaterials()
            => _productMaterialDao.GetProductMaterials();
    }
}
