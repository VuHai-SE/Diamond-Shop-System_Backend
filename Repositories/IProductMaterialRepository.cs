using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IProductMaterialRepository
    {
        public List<TblProductMaterial> GetProductMaterials();
        public TblProductMaterial GetProductMaterialProductID(string productID);
        public List<TblProductMaterial> GetProductMaterialByMaterialID(string materialID);
    }
}
