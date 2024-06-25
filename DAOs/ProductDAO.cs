using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace DAOs
{
    public class ProductDAO
    {
        private readonly DiamondStoreContext _context = null;

        public ProductDAO()
        {
            _context = new DiamondStoreContext();
        }

        public TblProduct AddProduct(TblProduct product)
        {
            _context.TblProducts.Add(product);
            _context.SaveChanges();
            return product;
        }

        //Tạo Product mới
        public async Task<GenericResponse> CreateProductAsync(CreateProductRequest request)
        {
            // Bước 1: Kiểm tra các trường có trống không
            if (string.IsNullOrEmpty(request.ProductName) ||
                string.IsNullOrEmpty(request.ProductCode) ||
                string.IsNullOrEmpty(request.Description) ||
                string.IsNullOrEmpty(request.CategoryID) ||
                request.GemCost <= 0 ||
                request.ProductionCost <= 0 ||
                request.PriceRate <= 0 ||
                request.ProductSize <= 0 ||
                string.IsNullOrEmpty(request.Image) ||
                (request.Status != 1 && request.Status != 0) ||
                (request.Gender != -1 && request.Gender != 0 && request.Gender != 1) ||
                string.IsNullOrEmpty(request.GemId) ||
                string.IsNullOrEmpty(request.MaterialId) ||
                request.Weight <= 0)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "All fields are required."
                };
            }

            // Bước 2: Kiểm tra sự tồn tại của CategoryID, GemId, MaterialId
            var categoryExists = await _context.TblProductCategories.AnyAsync(c => c.CategoryId == request.CategoryID);
            if (!categoryExists)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "CategoryID not found."
                };
            }

            var gemExists = await _context.TblGems.AnyAsync(g => g.GemId == request.GemId);
            if (!gemExists)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "GemID not found."
                };
            }

            var materialExists = await _context.TblMaterialCategories.AnyAsync(m => m.MaterialId == request.MaterialId);
            if (!materialExists)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "MaterialID not found."
                };
            }

            // Bước 3: Tự động tạo mới productId
            //var productCount = await _context.TblProducts.CountAsync();
            //var newProductId = $"P{(productCount + 1).ToString("D3")}";
            var maxProductId = await _context.TblProducts
                                 .OrderByDescending(p => p.ProductId)
                                 .Select(p => p.ProductId)
                                 .FirstOrDefaultAsync();

            int nextProductNumber;
            if (maxProductId == null)
            {
                nextProductNumber = 1;
            }
            else
            {
                nextProductNumber = int.Parse(maxProductId.Substring(1)) + 1;
            }

            var newProductId = $"P{nextProductNumber.ToString("D3")}";

            //Bước 4: Update MaterialCost and UnitPriceSize
            double materialPrice = 0;
            var materialPriceList = GetMaterialPriceList(request.MaterialId);
            var latestMaterialPrice = materialPriceList.FirstOrDefault()?.UnitPrice ?? 0;
            materialPrice += (request.Weight) * latestMaterialPrice;

            // Bước 5: Tạo mới Product
            var product = new TblProduct
            {
                ProductId = newProductId,
                ProductName = request.ProductName,
                ProductCode = request.ProductCode,
                Description = request.Description,
                CategoryId = request.CategoryID,
                MaterialCost = materialPrice,
                GemCost = (double)request.GemCost,
                ProductionCost = (double)request.ProductionCost,
                PriceRate = (double)request.PriceRate,
                ProductSize = request.ProductSize,
                Image = request.Image,
                Status = Convert.ToBoolean(request.Status),
                UnitSizePrice = (double)materialPrice / request.ProductSize,
                Gender = request.Gender
            };

            await _context.TblProducts.AddAsync(product);
            await _context.SaveChangesAsync();

            // Bước 6: Cập nhật Tbl_ProductGem
            var productGem = new TblProductGem
            {
                ProductId = newProductId,
                GemId = request.GemId
            };

            await _context.TblProductGems.AddAsync(productGem);

            // Bước 7: Cập nhật Tbl_ProductMaterial
            var productMaterial = new TblProductMaterial
            {
                ProductId = newProductId,
                MaterialId = request.MaterialId,
                Weight = request.Weight
            };

            await _context.TblProductMaterials.AddAsync(productMaterial);
            await _context.SaveChangesAsync();

            return new GenericResponse
            {
                Success = true,
                Message = "Product created successfully."
            };
        }

        //Update Product
        public async Task<GenericResponse> UpdateProductAsync(string productId, CreateProductRequest request)
        {
            // Bước 1: Kiểm tra sự tồn tại của ProductId, CategoryID, GemId, MaterialId
            var existingProduct = await GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "ProductID not found."
                };
            }

            var categoryExists = await _context.TblProductCategories.AnyAsync(c => c.CategoryId == request.CategoryID);
            if (!categoryExists)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "CategoryID not found."
                };
            }

            var gemExists = await _context.TblGems.AnyAsync(g => g.GemId == request.GemId);
            if (!gemExists)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "GemID not found."
                };
            }

            var materialExists = await _context.TblMaterialCategories.AnyAsync(m => m.MaterialId == request.MaterialId);
            if (!materialExists)
            {
                return new GenericResponse
                {
                    Success = false,
                    Message = "MaterialID not found."
                };
            }

            // Bước 2: Update MaterialCost and UnitPriceSize
            double materialPrice = 0;
            var materialPriceList = GetMaterialPriceList(request.MaterialId);
            var latestMaterialPrice = materialPriceList.FirstOrDefault()?.UnitPrice ?? 0;
            materialPrice += (request.Weight) * latestMaterialPrice;

            // Bước 3: Update Product
            existingProduct.ProductName = request.ProductName;
            existingProduct.ProductCode = request.ProductCode;
            existingProduct.Description = request.Description;
            existingProduct.CategoryId = request.CategoryID;
            existingProduct.MaterialCost = materialPrice;
            existingProduct.GemCost = (double)request.GemCost;
            existingProduct.ProductionCost = (double)request.ProductionCost;
            existingProduct.PriceRate = (double)request.PriceRate;
            existingProduct.ProductSize = request.ProductSize;
            existingProduct.Image = request.Image;
            existingProduct.Status = Convert.ToBoolean(request.Status);
            existingProduct.UnitSizePrice = (double)materialPrice / request.ProductSize;
            existingProduct.Gender = request.Gender;

            _context.TblProducts.Update(existingProduct);
            await _context.SaveChangesAsync();

            // Bước 4: Cập nhật Tbl_ProductGem
            var existingProductGem = await _context.TblProductGems
                .FirstOrDefaultAsync(pg => pg.ProductId == productId);

            if (existingProductGem != null)
            {
                existingProductGem.GemId = request.GemId;
                _context.TblProductGems.Update(existingProductGem);
            }
            else
            {
                var newProductGem = new TblProductGem
                {
                    ProductId = productId,
                    GemId = request.GemId
                };
                await _context.TblProductGems.AddAsync(newProductGem);
            }

            // Bước 5: Cập nhật Tbl_ProductMaterial
            var existingProductMaterial = await _context.TblProductMaterials
                .FirstOrDefaultAsync(pm => pm.ProductId == productId);

            if (existingProductMaterial != null)
            {
                existingProductMaterial.MaterialId = request.MaterialId;
                existingProductMaterial.Weight = request.Weight;
                _context.TblProductMaterials.Update(existingProductMaterial);
            }
            else
            {
                var newProductMaterial = new TblProductMaterial
                {
                    ProductId = productId,
                    MaterialId = request.MaterialId,
                    Weight = request.Weight
                };
                await _context.TblProductMaterials.AddAsync(newProductMaterial);
            }

            await _context.SaveChangesAsync();

            return new GenericResponse
            {
                Success = true,
                Message = "Product updated successfully."
            };
        }

        public async Task UpdateAsync(string id, TblProduct product)
        {
            var existingProduct = await GetProductByIdAsync(id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductCode = product.ProductCode;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.MaterialCost = product.MaterialCost;
                existingProduct.GemCost = product.GemCost;
                existingProduct.ProductionCost = product.ProductionCost;
                existingProduct.PriceRate = product.PriceRate;
                existingProduct.ProductSize = product.ProductSize;
                existingProduct.Image = product.Image;
                existingProduct.Status = product.Status;
                existingProduct.UnitSizePrice = product.UnitSizePrice;
                existingProduct.Gender = product.Gender;

                _context.TblProducts.Update(existingProduct);
            }
        }

        public async Task AddAsync(TblProduct product)
        {
            await _context.TblProducts.AddAsync(product);
        }

        public async Task<TblProduct> GetProductByIdAsync(string productId)
        {
            return await _context.TblProducts.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<List<TblGem>> GetGemsByProductIdAsync(string productId)
        {
            return await (from gem in _context.TblGems
                          join productGem in _context.TblProductGems on gem.GemId equals productGem.GemId
                          where productGem.ProductId == productId
                          select gem).AsNoTracking().ToListAsync();
        }

        public IQueryable<TblGemPriceList> GetGemPriceList(TblGem gem)
        {
            return _context.TblGemPriceLists
                .Where(gpl => gpl.Origin == gem.Origin &&
                              gpl.CaratWeight == gem.CaratWeight &&
                              gpl.Color == gem.Color &&
                              gpl.Cut == gem.Cut &&
                              gpl.Clarity == gem.Clarity)
                .OrderByDescending(gpl => gpl.EffDate).AsNoTracking();
        }

        public async Task<List<TblProductMaterial>> GetProductMaterialsAsync(string productId)
        {
            return await _context.TblProductMaterials
                .Where(pm => pm.ProductId == productId)
                .AsNoTracking().ToListAsync();
        }

        public IQueryable<TblMaterialPriceList> GetMaterialPriceList(string materialId)
        {
            return _context.TblMaterialPriceLists
                .Where(mpl => mpl.MaterialId == materialId)
                .OrderByDescending(mpl => mpl.EffDate)
                .AsNoTracking();
        }

        //Get All Products
        public List<TblProduct> GetAllProducts()
        {
            return _context.TblProducts.ToList();
        }

        public List<TblProduct> filterProductsByCategoryID(string categoryID)
            => _context.TblProducts.Where(p => p.CategoryId.Equals(categoryID)).ToList();

        //Get product detail
        public TblProduct GetProduct(string id)
        {
            return _context.TblProducts.FirstOrDefault(m => m.ProductId.Equals(id));
        }

        public List<TblProduct> GetProductsByName(string name)
            => _context.TblProducts.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToList();

        public async Task<bool> UpdateProduct(string productID, TblProduct product)
        {
            var oProduct = await GetProductByIdAsync(productID);
            if (oProduct == null)
            {
                return false;
            }

            oProduct.ProductName = product.ProductName;
            oProduct.ProductCode = product.ProductCode;
            oProduct.Description = product.Description;
            oProduct.CategoryId = product.CategoryId;
            oProduct.MaterialCost = product.MaterialCost;
            oProduct.GemCost = product.GemCost;
            oProduct.ProductionCost = product.ProductionCost;
            oProduct.PriceRate = product.PriceRate;
            oProduct.ProductSize = product.ProductSize;
            oProduct.Image = product.Image;
            oProduct.Status = product.Status;
            oProduct.UnitSizePrice = product.UnitSizePrice;
            oProduct.Gender = product.Gender;
            _context.TblProducts.Update(oProduct);
            _context.SaveChanges();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
