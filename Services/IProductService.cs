using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services
{
    public interface IProductService
    {
        public TblProduct AddProduct(TblProduct product);

        Task<double> CalculateProductPriceAsync(string productId);

        Task<List<ProductWithPriceResponse>> GetAllProductsAndPricesAsync();
        Task<List<ProductWithPriceResponse>> FilterProducts(ProductFilterCriteria criteria);
        Task<ProductWithPriceResponse> GetProductAndPriceByIdAsync(string productId);

        public TblProduct GetProduct(string id);
        public Task<List<ProductWithPriceResponse>> filterProductsByCategoryID(string categoryID);
        public Task<List<ProductWithPriceResponse>> GetProductsByName(string name);
        public List<TblProduct> GetAllProducts();
        Task<bool> UpdateMaterialPriceAndUnitPriceSize(string productID, TblMaterialPriceList materialPriceList);
        Task<bool> UpdateProductStatus(string productID);
    }
}
