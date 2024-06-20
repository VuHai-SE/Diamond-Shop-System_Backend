using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class ProductMaterialDAO
    {
        private readonly DiamondStoreContext _context;

        public ProductMaterialDAO(DiamondStoreContext context)
        {
            _context = context;
        }

        public List<TblProductMaterial> GetProductMaterials()
            => _context.TblProductMaterials.ToList();

        public TblProductMaterial GetProductMaterialProductID(string productID)
            => _context.TblProductMaterials.FirstOrDefault(pm => pm.ProductId.Equals(productID));

        public List<TblProductMaterial> GetProductMaterialByMaterialID(string materialID)
            => _context.TblProductMaterials.Where(pm => pm.MaterialId.Equals(materialID)).ToList();
    }
}
