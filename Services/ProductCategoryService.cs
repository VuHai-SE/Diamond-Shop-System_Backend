using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository productCategoryRepository = null;

        public ProductCategoryService()
        {
            if (productCategoryRepository == null)
            {
                productCategoryRepository = new ProductCategoryRepository();
            }
        }

        public TblProductCategory AddProductCategories(TblProductCategory productCategory)
            => productCategoryRepository.AddProductCategories(productCategory);

        public bool DeleteProductCategory(string id)
            => productCategoryRepository.DeleteProductCategory(id);

        public List<TblProductCategory> GetProductCategories()
            => productCategoryRepository.GetProductCategories();

        public TblProductCategory GetProductCategory(string id)
            => productCategoryRepository.GetProductCategory(id);

        public bool UpdateProductCategory(string id, TblProductCategory productCategory)
            => productCategoryRepository.UpdateProductCategory(id, productCategory);
    }
}
