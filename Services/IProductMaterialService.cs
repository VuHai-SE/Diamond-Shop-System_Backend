using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IProductMaterialService
    {
        public List<TblProductMaterial> GetProductMaterials();
        public TblProductMaterial GetProductMaterialProductID(string productID);
        public List<TblProductMaterial> GetProductMaterialByMaterialID(string materialID);
    }
}
