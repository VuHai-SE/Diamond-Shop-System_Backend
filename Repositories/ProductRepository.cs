using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO productDAO;

        public ProductRepository()
        {
            if (productDAO == null)
            {
                productDAO = new ProductDAO();
            }
        }
        public TblProduct AddProduct(TblProduct product)
            => productDAO.AddProduct(product);

        public bool DeleteProduct(string id, TblProduct product)
            => productDAO.DeleteProduct(id, product);

        public TblProduct GetProduct(string id)
            => productDAO.GetProduct(id);

        public List<TblProduct> GetProducts()
            => productDAO.GetProducts();

        public bool UpdateProduct(string id, TblProduct product)
            => productDAO.UpdateProduct(id, product);
    }
}
