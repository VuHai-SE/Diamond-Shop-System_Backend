using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DAOs;
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
        private readonly IMaterialPriceListRepository materialPriceListRepository;
        public ProductService(IProductRepository _productRepository, IProductCategoryRepository _productCategoryRepository, IProductMaterialRepository _productMaterialRepository, IMaterialCategoryRepository _materialCategoryRepository, IGemRepository _gemRepository, IMaterialPriceListRepository _materialPriceLisRepository)
        {
            productRepository = _productRepository;
            productCategoryRepository = _productCategoryRepository;
            productMaterialRepository = _productMaterialRepository;
            materialCategoryRepository = _materialCategoryRepository;
            gemRepository = _gemRepository;
            materialPriceListRepository = _materialPriceLisRepository;
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
                productWithPriceList = productWithPriceList.Where(p => p.Category != null && p.Category.Equals(criteria.Category.Trim())).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Material))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Material != null && p.Material.Equals(criteria.Material.Trim())).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.GemOrigin))
            {
                productWithPriceList = productWithPriceList.Where(p => p.GemOrigin != null && p.GemOrigin.Equals(criteria.GemOrigin.Trim())).ToList();
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
                productWithPriceList = productWithPriceList.Where(p => p.Cut != null && p.Cut.Equals(criteria.Cut.Trim())).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Clarity))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Clarity != null && p.Clarity.Equals(criteria.Clarity.Trim())).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Color))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Color != null && p.Color.Equals(criteria.Color.Trim())).ToList();
            }

            if (!string.IsNullOrEmpty(criteria.Gender))
            {
                productWithPriceList = productWithPriceList.Where(p => p.Gender != null && p.Gender.Equals(criteria.Gender.Trim())).ToList();
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
                Cut = gem.Cut,
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

        public async Task<TblProduct> GetProductByIdAsync(string id)
        {
            return await productRepository.GetProductByIdAsync(id);
        }

        public async Task<bool> UpdateMaterialPriceAndUnitPriceSize(string productID, TblMaterialPriceList materialPriceList)
        {
            var product = await productRepository.GetProductByIdAsync(productID);
            if (product == null)
            {
                return false;
            }
            var productMaterial = productMaterialRepository.GetProductMaterialProductID(productID);
            var materialCost = materialPriceList.UnitPrice * productMaterial.Weight;
            product.MaterialCost = materialCost;
            product.UnitSizePrice = materialCost / product.ProductSize;
            return await productRepository.UpdateProduct(productID, product);
        }

        public async Task<bool> UpdateProductStatus(string productID)
        {
            var product = await productRepository.GetProductByIdAsync(productID);
            if (product == null)
            {
                return false;
            }
            product.Status = false;
            return await productRepository.UpdateProduct(productID, product);
        }

        public async Task<GenericResponse> CreateProductAsync(CreateProductRequest request)
        {
            return await productRepository.CreateProductAsync(request);
        }

        public async Task<bool> UpdateProductAsync(string id, TblProduct product)
        {
            var existingProduct = await productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return false;
            }

            await productRepository.UpdateAsync(id, product);

            return true;
        }
    }
}
