using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ProductCategoryDAO productCategoryDAO = null;

        public ProductCategoryRepository()
        {
            if (productCategoryDAO == null)
            {
                productCategoryDAO = new ProductCategoryDAO();
            }
        }

        public TblProductCategory AddProductCategories(TblProductCategory productCategory)
            => productCategoryDAO.AddProductCategories(productCategory);

        public bool DeleteProductCategory(string id)
            => productCategoryDAO.DeleteProductCategory(id);

        public List<TblProductCategory> GetProductCategories()
            => productCategoryDAO.GetProductCategories();

        public TblProductCategory GetProductCategory(string id)
            => productCategoryDAO.GetProductCategory(id);

        public bool UpdateProductCategory(string id, TblProductCategory productCategory)
            => productCategoryDAO.UpdateProductCategory(id, productCategory);
    }
}
