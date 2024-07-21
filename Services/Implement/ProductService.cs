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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Implement;
using Services.DTOs.Request;
using Services.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using DAOs.DTOs.Response;

namespace Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly DiamondStoreContext _context;
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IProductMaterialRepository productMaterialRepository;
        private readonly IMaterialCategoryRepository materialCategoryRepository;
        private readonly IGemRepository gemRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IOrderDetailRepository orderDetailRepository;
        //private readonly DiamondStoreContext db = null;
        public ProductService(IProductRepository _productRepository, IProductCategoryRepository _productCategoryRepository, IProductMaterialRepository _productMaterialRepository, IMaterialCategoryRepository _materialCategoryRepository, IGemRepository _gemRepository, ILogger<ProductService> logger, DiamondStoreContext context, IOrderDetailRepository _orderDetailRepository)
        {
            productRepository = _productRepository;
            productCategoryRepository = _productCategoryRepository;
            productMaterialRepository = _productMaterialRepository;
            materialCategoryRepository = _materialCategoryRepository;
            gemRepository = _gemRepository;
            _logger = logger;
            _context = context;
            orderDetailRepository = _orderDetailRepository;
        }

        //public ProductService()
        //{
        //    if (db == null)
        //    {
        //        db = new();
        //    }
        //}

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
                if (productWithPrice != null)
                {
                    productWithPriceList.Add(productWithPrice);
                }
            }
            return productWithPriceList;
        }

        public async Task<List<ProductWithPriceResponse>> FilterProducts(ProductFilterCriteria criteria)
        {
            var query = from p in _context.TblProducts
                        join pm in _context.TblProductMaterials on p.ProductId equals pm.ProductId into pmJoin
                        from pm in pmJoin.DefaultIfEmpty()
                        join pg in _context.TblProductGems on p.ProductId equals pg.ProductId into pgJoin
                        from pg in pgJoin.DefaultIfEmpty()
                        join c in _context.TblProductCategories on p.CategoryId equals c.CategoryId into cJoin
                        from c in cJoin.DefaultIfEmpty()
                        join m in _context.TblMaterialCategories on pm.MaterialId equals m.MaterialId into mJoin
                        from m in mJoin.DefaultIfEmpty()
                        join g in _context.TblGems on pg.GemId equals g.GemId into gJoin
                        from g in gJoin.DefaultIfEmpty()
                        select new { p, pm, pg, c, m, g };

            if (!string.IsNullOrEmpty(criteria.Category))
            {
                query = query.Where(x => x.c != null && x.c.CategoryName.Equals(criteria.Category.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.Material))
            {
                query = query.Where(x => x.m != null && x.m.MaterialName.Equals(criteria.Material.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.GemOrigin))
            {
                query = query.Where(x => x.g != null && ((bool)x.g.Origin ? "Natural" : "Synthetic").Equals(criteria.GemOrigin.Trim()));
            }

            if (criteria.MinCaratWeight.HasValue)
            {
                query = query.Where(x => x.g != null && x.g.CaratWeight >= criteria.MinCaratWeight);
            }

            if (criteria.MaxCaratWeight.HasValue)
            {
                query = query.Where(x => x.g != null && x.g.CaratWeight <= criteria.MaxCaratWeight);
            }

            if (!string.IsNullOrEmpty(criteria.Cut))
            {
                query = query.Where(x => x.g != null && x.g.Cut.Equals(criteria.Cut.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.Clarity))
            {
                query = query.Where(x => x.g != null && x.g.Clarity.Equals(criteria.Clarity.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.Color))
            {
                query = query.Where(x => x.g != null && x.g.Color.Equals(criteria.Color.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.Gender))
            {
                query = query.Where(x => x.p.Gender != null && x.p.Gender.Equals(criteria.Gender.Trim()));
            }

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)criteria.PageSize);

            var products = await query
                .Skip((criteria.PageNumber - 1) * criteria.PageSize)
                .Take(criteria.PageSize)
                .Select(x => new ProductWithPriceResponse
                {
                    ProductId = x.p.ProductId,
                    ProductName = x.p.ProductName,
                    ProductCode = x.p.ProductCode,
                    Description = x.p.Description,
                    Category = x.c != null ? x.c.CategoryName : string.Empty,
                    Material = x.m != null ? x.m.MaterialName : string.Empty,
                    GemOrigin = x.g != null ? ((bool)x.g.Origin ? "Natural" : "Synthetic") : string.Empty,
                    CaratWeight = x.g != null ? x.g.CaratWeight : 0,
                    Clarity = x.g != null ? x.g.Clarity : string.Empty,
                    Color = x.g != null ? x.g.Color : string.Empty,
                    Cut = x.g != null ? x.g.Cut : string.Empty,
                    ProductSize = x.p.ProductSize,
                    Image = x.p.Image,
                    Status = x.p.Status,
                    UnitSizePrice = x.p.UnitSizePrice,
                    Gender = x.p.Gender == 1 ? "Male" : (x.p.Gender == 0 ? "Unisex" : "Female")
                }).ToListAsync();

            foreach (var product in products)
            {
                product.ProductPrice = await CalculateProductPriceAsync(product.ProductId);
            }
            return products;
        }

        public async Task<int> CountFilteredProducts(ProductFilterCriteria criteria)
        {
            var query = from p in _context.TblProducts
                        join pm in _context.TblProductMaterials on p.ProductId equals pm.ProductId into pmJoin
                        from pm in pmJoin.DefaultIfEmpty()
                        join pg in _context.TblProductGems on p.ProductId equals pg.ProductId into pgJoin
                        from pg in pgJoin.DefaultIfEmpty()
                        join c in _context.TblProductCategories on p.CategoryId equals c.CategoryId into cJoin
                        from c in cJoin.DefaultIfEmpty()
                        join m in _context.TblMaterialCategories on pm.MaterialId equals m.MaterialId into mJoin
                        from m in mJoin.DefaultIfEmpty()
                        join g in _context.TblGems on pg.GemId equals g.GemId into gJoin
                        from g in gJoin.DefaultIfEmpty()
                        select new { p, pm, pg, c, m, g };

            if (!string.IsNullOrEmpty(criteria.Category))
            {
                query = query.Where(x => x.c != null && x.c.CategoryName.Equals(criteria.Category.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.Material))
            {
                query = query.Where(x => x.m != null && x.m.MaterialName.Equals(criteria.Material.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.GemOrigin))
            {
                query = query.Where(x => x.g != null && ((bool)x.g.Origin ? "Natural" : "Synthetic").Equals(criteria.GemOrigin.Trim()));
            }

            if (criteria.MinCaratWeight.HasValue)
            {
                query = query.Where(x => x.g != null && x.g.CaratWeight >= criteria.MinCaratWeight);
            }

            if (criteria.MaxCaratWeight.HasValue)
            {
                query = query.Where(x => x.g != null && x.g.CaratWeight <= criteria.MaxCaratWeight);
            }

            if (!string.IsNullOrEmpty(criteria.Cut))
            {
                query = query.Where(x => x.g != null && x.g.Cut.Equals(criteria.Cut.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.Clarity))
            {
                query = query.Where(x => x.g != null && x.g.Clarity.Equals(criteria.Clarity.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.Color))
            {
                query = query.Where(x => x.g != null && x.g.Color.Equals(criteria.Color.Trim()));
            }

            if (!string.IsNullOrEmpty(criteria.Gender))
            {
                query = query.Where(x => x.p.Gender != null && x.p.Gender.Equals(criteria.Gender.Trim()));
            }

            return await query.CountAsync();
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

            if (productMaterial == null || gem == null)
            {
                _logger.LogError("Product material or gem is null for product ID: {ProductId}", productId);
                return null;
            }

            return new ProductWithPriceResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductCode = product.ProductCode,
                Description = product.Description,
                CategoryID = product.CategoryId,//add mới cho Triều
                GemCost = (decimal?)product.GemCost, //add mới cho Triều
                ProductionCost = (decimal?)product.ProductionCost, //add mới cho Triều
                PriceRate = (decimal?)product.PriceRate, //add mới cho Triều
                GemId = gem.GemId,//add mới cho Triều
                MaterialId = productMaterial.MaterialId,//add mới cho Triều
                Weight = productMaterial.Weight,//add mới cho Triều
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
            // Truy vấn sản phẩm với AsNoTracking để tránh theo dõi bởi ngữ cảnh

            var product = await productRepository.GetProductByIdAsync(productID);
            if (product == null)
            {
                return false;
            }

            // Cập nhật trạng thái của sản phẩm
            product.Status = !product.Status;
            await productRepository.UpdateAsync(product.ProductId, product);
            return true;
        }

        public async Task<GenericResponse> CreateProductAsync(CreateProductRequest request)
        {
            return await productRepository.CreateProductAsync(request);
        }

        public async Task<GenericResponse> UpdateProductAsync(string productId, CreateProductRequest request)
        {
            return await productRepository.UpdateProductAsync(productId, request);
        }

        public async Task<GenericResponse> DeleteProductAsync(string productId)
        {
            return await productRepository.DeleteProductAsync(productId);
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

        public async Task<ProductCount> GetProductsCountAsync()
            => await productRepository.GetProductsCountAsync();

        public async Task<string> GetMostSoldProductCategoryOfMonthYear(int month, int year)
            => await productRepository.GetMostSoldProductCategoryOfMonthYear(month, year);

        public async Task UpdateProductStatusByCancelOrder(int orderId)
        {
            var productsBuying = orderDetailRepository.GetOrderDetailsByOrderID(orderId);
            
            foreach (var p in productsBuying)
            {
                var product = await productRepository.GetProductByIdAsync(p.ProductId);
                product.Status = true;
                await productRepository.UpdateProduct(product.ProductId, product);
            }
        }
    }
}
