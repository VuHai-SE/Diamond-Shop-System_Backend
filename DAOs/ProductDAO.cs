using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class ProductDAO
    {
        private readonly DiamondStoreContext dbContext = null;

        public ProductDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public List<TblProduct> GetProducts()
            => dbContext.TblProducts.ToList();

        public TblProduct GetProduct(string id)
            => dbContext.TblProducts.FirstOrDefault(m => m.ProductId.Equals(id));

        public TblProduct AddProduct(TblProduct product)
        {
            dbContext.TblProducts.Add(product);
            dbContext.SaveChanges();
            return product;
        }

        public bool UpdateProduct(string id,  TblProduct product)
        {
            return false;
        }

        public bool DeleteProduct(string id, TblProduct product)
        {
            return false;
        }
    }
}
