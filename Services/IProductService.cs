using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DAOs.DTOs.Response;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services
{
    public interface IProductService
    {
        public TblProduct AddProduct(TblProduct product);

        Task<double> CalculateProductPriceAsync(string productId);

        Task<GenericResponse> CreateProductAsync(CreateProductRequest request);

        Task<GenericResponse> UpdateProductAsync(string productId, CreateProductRequest request);

        Task<GenericResponse> DeleteProductAsync(string productId);

        Task<bool> UpdateProductAsync(string id, TblProduct product);

        Task<List<ProductWithPriceResponse>> GetAllProductsAndPricesAsync();
        Task<List<ProductWithPriceResponse>> FilterProducts(ProductFilterCriteria criteria);
        Task<int> CountFilteredProducts(ProductFilterCriteria criteria);
        Task<ProductWithPriceResponse> GetProductAndPriceByIdAsync(string productId);

        public TblProduct GetProduct(string id);

        Task<TblProduct> GetProductByIdAsync(string id);

        public Task<List<ProductWithPriceResponse>> filterProductsByCategoryID(string categoryID);
        public Task<List<ProductWithPriceResponse>> GetProductsByName(string name);
        public List<TblProduct> GetAllProducts();
        Task<bool> UpdateMaterialPriceAndUnitPriceSize(string productID, TblMaterialPriceList materialPriceList);
        Task<bool> UpdateProductStatus(string productID);
        public Task<ProductCount> GetProductsCountAsync();
        public Task<string> GetMostSoldProductCategoryOfMonthYear(int month, int year);
        public Task UpdateProductStatusByCancelOrder(int orderId);
    }
}
