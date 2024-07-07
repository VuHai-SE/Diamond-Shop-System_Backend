using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository _productCategoryRepository)
        {
            productCategoryRepository = _productCategoryRepository;
        }

        public TblProductCategory AddProductCategories(TblProductCategory productCategory)
            => productCategoryRepository.AddProductCategories(productCategory);

        public bool DeleteProductCategory(string id)
            => productCategoryRepository.DeleteProductCategory(id);

        public TblProductCategory GetCategoryByName(string name)
            => productCategoryRepository.GetCategoryByName(name);

        public List<TblProductCategory> GetProductCategories()
            => productCategoryRepository.GetProductCategories();

        public TblProductCategory GetProductCategory(string id)
            => productCategoryRepository.GetProductCategory(id);

        public bool UpdateProductCategory(string id, TblProductCategory productCategory)
            => productCategoryRepository.UpdateProductCategory(id, productCategory);
    }
}
