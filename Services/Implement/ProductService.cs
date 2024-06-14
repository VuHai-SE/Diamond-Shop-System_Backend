using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IProductMaterialRepository productMaterialRepository;
        private readonly IMaterialCategoryRepository materialCategoryRepository;
        public ProductService(IProductRepository _productRepository, IProductCategoryRepository _productCategoryRepository, IProductMaterialRepository _productMaterialRepository, IMaterialCategoryRepository _materialCategoryRepository)
        {
            productRepository = _productRepository;
            productCategoryRepository = _productCategoryRepository;
            productMaterialRepository = _productMaterialRepository;
            materialCategoryRepository = _materialCategoryRepository;
        }

        public async Task<double> CalculateProductPriceAsync(string productId)
        {
            return await productRepository.CalculateProductPriceAsync(productId);
        }

        public TblProduct AddProduct(TblProduct product)
            => productRepository.AddProduct(product);

        public async Task<List<ProductWithPriceResponse>> GetAllProductsAndPricesAsync()
        {
            var productWithPriceList = new List<ProductWithPriceResponse>();
            var productList = GetAllProducts();
            foreach (var product in productList)
            {
                var productWithPrice = await GetProductAndPriceByIdAsync(product.ProductId);
                productWithPriceList.Add(productWithPrice);
            }
            return productWithPriceList;
        }

        public async Task<ProductWithPriceResponse> GetProductAndPriceByIdAsync(string productId)
        {
            var product = await productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return null;
            }

            var price = await productRepository.CalculateProductPriceAsync(productId);
            var productMaterial = productMaterialRepository.GetProductMaterialProductID(productId);
            return new ProductWithPriceResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductCode = product.ProductCode,
                Description = product.Description,
                Category = productCategoryRepository.GetProductCategory(product.CategoryId).CategoryName,
                Material = materialCategoryRepository.GetMaterialCategory(productMaterial.MaterialId).MaterialName,
                MaterialCost = product.MaterialCost,
                GemCost = product.GemCost,
                ProductionCost = product.ProductionCost,
                PriceRate = product.PriceRate,
                ProductSize = product.ProductSize,
                Image = product.Image,
                Status = product.Status,
                UnitSizePrice = product.UnitSizePrice,
                ProductPrice = price
            };
        }

        public TblProduct GetProduct(string id)
            => productRepository.GetProduct(id);

        public List<TblProduct> filterProductsByCategoryID(string categoryID)
            => productRepository.filterProductsByCategoryID(categoryID);

        public List<TblProduct> GetProductsByName(string name)
            => productRepository.GetProductsByName(name);

        public List<TblProduct> GetAllProducts()
            => productRepository.GetAllProducts();
    }
}
