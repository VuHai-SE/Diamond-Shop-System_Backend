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

        public async Task<double> CalculateProductPriceAsync(string productId)
        {
            return await productRepository.CalculateProductPriceAsync(productId);
        }

        public TblProduct AddProduct(TblProduct product)
            => productRepository.AddProduct(product);

        public TblProduct GetProduct(string id)
            => productRepository.GetProduct(id);

        public List<TblProduct> GetProducts()
            => productRepository.GetProducts();

    }
}
