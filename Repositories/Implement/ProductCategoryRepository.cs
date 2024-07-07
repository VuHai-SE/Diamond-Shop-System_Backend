using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ProductCategoryDAO productCategoryDAO;

        public ProductCategoryRepository(ProductCategoryDAO _productCategoryDAO)
        {
            productCategoryDAO = _productCategoryDAO;
        }

        public TblProductCategory AddProductCategories(TblProductCategory productCategory)
            => productCategoryDAO.AddProductCategories(productCategory);

        public bool DeleteProductCategory(string id)
            => productCategoryDAO.DeleteProductCategory(id);

        public TblProductCategory GetCategoryByName(string name)
            => productCategoryDAO.GetCategoryByName(name);

        public List<TblProductCategory> GetProductCategories()
            => productCategoryDAO.GetProductCategories();

        public TblProductCategory GetProductCategory(string id)
            => productCategoryDAO.GetProductCategory(id);

        public bool UpdateProductCategory(string id, TblProductCategory productCategory)
            => productCategoryDAO.UpdateProductCategory(id, productCategory);
    }
}
