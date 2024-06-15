using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Implement;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IProductMaterialRepository productMaterialRepository;
        private readonly IMaterialCategoryRepository materialCategoryRepository;
        private readonly IGemRepository gemRepository;
        public ProductService(IProductRepository _productRepository, IProductCategoryRepository _productCategoryRepository, IProductMaterialRepository _productMaterialRepository, IMaterialCategoryRepository _materialCategoryRepository, IGemRepository _gemRepository)
        {
            productRepository = _productRepository;
            productCategoryRepository = _productCategoryRepository;
            productMaterialRepository = _productMaterialRepository;
            materialCategoryRepository = _materialCategoryRepository;
            gemRepository = _gemRepository;
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

        public async Task<List<ProductWithPriceResponse>> FilterProducts(ProductFilterCriteria criteria)
        {
            var productWithPriceList = await GetAllProductsAndPricesAsync();
            if (!string.IsNullOrEmpty(criteria.Category))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Category != null && p.Category.Contains(criteria.Category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Material))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Material != null && p.Material.Contains(criteria.Material, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.GemOrigin))
            {
                productWithPriceList = productWithPriceList.Where(p => p.GemOrigin != null && p.GemOrigin.Contains(criteria.GemOrigin, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (criteria.MinCaratWeight.HasValue)
            {
                productWithPriceList = productWithPriceList.Where(p => p.CaratWeight.HasValue && p.CaratWeight >= criteria.MinCaratWeight).ToList();
            }

            if (criteria.MaxCaratWeight.HasValue)
            {
                productWithPriceList = productWithPriceList.Where(p => p.CaratWeight.HasValue && p.CaratWeight <= criteria.MaxCaratWeight).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Cut))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Cut != null && p.Cut.Contains(criteria.Cut, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Clarity))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Clarity != null && p.Clarity.Contains(criteria.Clarity, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Color))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Color != null && p.Color.Contains(criteria.Color, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Gender))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Gender != null && p.Gender.Equals(criteria.Gender, StringComparison.OrdinalIgnoreCase)).ToList();
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
            var gem = gemRepository.GetGemByProduct(productId);
            return new ProductWithPriceResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductCode = product.ProductCode,
                Description = product.Description,
                Category = productCategoryRepository.GetProductCategory(product.CategoryId).CategoryName,
                Material = materialCategoryRepository.GetMaterialCategory(productMaterial.MaterialId).MaterialName,
                GemOrigin = (gem.Origin == true) ? "Natural" : "Synthetic",
                CaratWeight = gem.CaratWeight,
                Clarity = gem.Clarity,
                Color = gem.Color,
                ProductSize = product.ProductSize,
                Image = product.Image,
                Status = product.Status,
                UnitSizePrice = product.UnitSizePrice,
                Gender = product.Gender == 1 ? "Male" : (product.Gender == 0 ? "Unisex" : "Female"),
                ProductPrice = price
            };
        }

        public TblProduct GetProduct(string id)
            => productRepository.GetProduct(id);

        public async Task<List<ProductWithPriceResponse>> filterProductsByCategoryID(string categoryID)
        {
            var productWithPriceList = new List<ProductWithPriceResponse>();
            var productList = productRepository.filterProductsByCategoryID(categoryID);
            foreach (var product in productList)
            {
                var productWithPrice = await GetProductAndPriceByIdAsync(product.ProductId);
                productWithPriceList.Add(productWithPrice);
            }
            return productWithPriceList;
        }

        public async Task<List<ProductWithPriceResponse>> GetProductsByName(string name)
        {
            var productWithPriceList = new List<ProductWithPriceResponse>();
            var productList = productRepository.GetProductsByName(name);
            foreach (var product in productList)
            {
                var productWithPrice = await GetProductAndPriceByIdAsync(product.ProductId);
                productWithPriceList.Add(productWithPrice);
            }
            return productWithPriceList;
        }

        public List<TblProduct> GetAllProducts()
            => productRepository.GetAllProducts();
    }
}
