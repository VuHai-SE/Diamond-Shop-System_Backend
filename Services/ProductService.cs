using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        
        public ProductService()
        {
            if (productRepository == null)
            {
                productRepository = new ProductRepository();
            }
        }

        public TblProduct AddProduct(TblProduct product)
            => productRepository.AddProduct(product);

        public bool DeleteProduct(string id, TblProduct product)
            => productRepository.DeleteProduct(id, product);

        public TblProduct GetProduct(string id)
            => productRepository.GetProduct(id);

        public List<TblProduct> GetProducts()
            => productRepository.GetProducts();

        public bool UpdateProduct(string id, TblProduct product)
            => productRepository.UpdateProduct(id, product);
    }
}
