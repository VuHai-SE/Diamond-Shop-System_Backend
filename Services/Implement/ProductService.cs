using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;
using Services.DTOs.Response;

namespace Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository = null;

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

        public async Task<List<(TblProduct product, double price)>> GetAllProductsAndPricesAsync()
        {
            return await productRepository.GetAllProductsAndPricesAsync();
        }

        public async Task<ProductWithPriceResponse> GetProductAndPriceByIdAsync(string productId)
        {
            var product = await productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return null;
            }

            var price = await productRepository.CalculateProductPriceAsync(productId);
            return new ProductWithPriceResponse
            {
                product = product,
                price = price
            };
        }

        public TblProduct GetProduct(string id)
            => productRepository.GetProduct(id);

        public List<TblProduct> filterProductsByCategoryID(string categoryID)
            => productRepository.filterProductsByCategoryID(categoryID);
    }
}
