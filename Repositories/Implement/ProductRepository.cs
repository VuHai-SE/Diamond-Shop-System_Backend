using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DAOs;
using DAOs.DTOs.Response;

namespace Repositories.Implement
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO productDAO;

        public ProductRepository(ProductDAO _productDAO)
        {
           productDAO = _productDAO;
        }
        public TblProduct AddProduct(TblProduct product)
            => productDAO.AddProduct(product);

        public async Task AddAsync(TblProduct product)
        {
            await productDAO.AddAsync(product);
        }

        public async Task<GenericResponse> CreateProductAsync(CreateProductRequest request)
        {
            return await productDAO.CreateProductAsync(request);
        }

        public async Task<GenericResponse> UpdateProductAsync(string productId, CreateProductRequest request)
        {
            return await productDAO.UpdateProductAsync(productId, request);
        }

        public async Task<GenericResponse> DeleteProductAsync(string productId)
        {
            return await productDAO.DeleteProductAsync(productId);
        }

        public async Task SaveChangesAsync()
        {
            await productDAO.SaveChangesAsync();
        }

        public async Task<double> CalculateProductPriceAsync(string productId)
        {
            var product = await productDAO.GetProductByIdAsync(productId);
            if (product == null) return 0;

            double gemTotalPrice = 0;
            var gems = await productDAO.GetGemsByProductIdAsync(productId);
            foreach (var gem in gems)
            {
                var gemPriceList = productDAO.GetGemPriceList(gem);
                var latestGemPrice = gemPriceList.FirstOrDefault()?.Price ?? 0;
                gemTotalPrice += latestGemPrice;
            }

            double materialTotalPrice = 0;
            var productMaterials = await productDAO.GetProductMaterialsAsync(productId);
            foreach (var pm in productMaterials)
            {
                var materialPriceList = productDAO.GetMaterialPriceList(pm.MaterialId);
                var latestMaterialPrice = materialPriceList.FirstOrDefault()?.UnitPrice ?? 0;
                materialTotalPrice += (pm.Weight ?? 0) * latestMaterialPrice;
            }
            var priceRate = 1 + (double)product.PriceRate / 100;
            return (gemTotalPrice + (product.GemCost ?? 0) + materialTotalPrice + (product.ProductionCost ?? 0)) * priceRate;
        }

        public List<TblProduct> filterProductsByCategoryID(string categoryID)
            => productDAO.filterProductsByCategoryID(categoryID);

        public List<TblProduct> GetAllProducts()
            => productDAO.GetAllProducts();

        //public async Task<List<(TblProduct product, double price)>> GetAllProductsAndPricesAsync()
        //{
        //    var products = productDAO.GetAllProducts();
        //    var productPrices = new List<(TblProduct product, double price)>();

        //    foreach (var product in products)
        //    {
        //        var price = await CalculateProductPriceAsync(product.ProductId);
        //        productPrices.Add((product, price));
        //    }

        //    return productPrices;
        //}

        public TblProduct GetProduct(string id)
            =>productDAO.GetProduct(id);

        public async Task<TblProduct> GetProductByIdAsync(string productId)
        {
            return await productDAO.GetProductByIdAsync(productId);
        }

        public List<TblProduct> GetProductsByName(string name)
            => productDAO.GetProductsByName(name);

        public Task<bool> UpdateProduct(string productID, TblProduct product)
        {
            return productDAO.UpdateProduct(productID, product);
        }

        public async Task UpdateAsync(string id, TblProduct product)
        {
            await productDAO.UpdateAsync(id, product);
            await productDAO.SaveChangesAsync(); // Gọi lưu thay đổi tại tầng DAO
        }

        public async Task<ProductCount> GetProductsCountAsync()
            => await productDAO.GetProductsCountAsync();

        public async Task<string> GetMostSoldProductCategoryOfMonthYear(int month, int year)
            => await productDAO.GetMostSoldProductCategoryOfMonthYear(month, year);
    }
}
